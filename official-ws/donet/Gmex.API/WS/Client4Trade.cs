using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gmex.API.WS
{
    public class Client4Trade
    {
        // 消息回掉对应关系表， 如果有线程安全需求，可以考虑换成 ConcurrentDictionary。
        private Dictionary<string, Action<int, object>> _request_callback = new Dictionary<string, Action<int, object>>();

        private ClientWebSocket _ws;

        private string _uname;
        private string _apikey;
        private string _apisecret;

        private string _uid; // Login成功后得到

        public Client4Trade(string uname, string apiKey, string apiSecret)
        {
            _uname = uname;
            _apikey = apiKey;
            _apisecret = apiSecret;
        }

        public void SafeClose()
        {
            _ws?.Dispose();
            _ws = null;
        }

        public string GetCurrentUId()
        {
            return _uid;
        }

        public bool IsConnected()
        {
            return _ws != null && _ws.State == WebSocketState.Open;
        }

        private Task SendRequestAsync(WsTradeMessageRequest msg, Action<int, object> action, CancellationToken cancellationToken)
        {
            if (this._ws == null || this._ws.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("websocket is not open");
            }

            try
            {
                _request_callback[msg.ReqID] = action;
                return this._ws?.SendAsync(new ArraySegment<byte>(msg.GetSigneBuffer(_apikey, _apisecret)), WebSocketMessageType.Text, true, cancellationToken);
            }
            catch (Exception ex)
            {
                _request_callback.Remove(msg.ReqID);
                throw ex;
            }
        }

        public async Task ReceiveLoop(string url, bool autoReconnect,
            Action<Models.Wlt> onWallet,
            Action<Models.TrdRec> onTrade,
            Action<Models.Ord> onOrder,
            Action<Models.Position> onPosition,
            Action<Models.WltLog> onWltLog,
            Action onDisconnected,
            Action onReconnected,
            CancellationToken cancellationToken)
        {
            try
            {
                _ws?.Dispose();
                _ws = new ClientWebSocket();
                await _ws.ConnectAsync(new Uri(url), cancellationToken);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            await Task.Factory.StartNew(async () =>
            {
                var buffer = new byte[GlobalDefine.K_MAX_MSG_SIZE]; // 有时消息会很大，比如查询1天的分钟K结果.
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Debug.WriteLine($"[NOTE] Trade WebSocket({url}) is Cancelled!");
                        await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                        onDisconnected?.Invoke();
                        return;
                    }

                    if (_ws.State != WebSocketState.Open)
                    {
                        if (!autoReconnect)
                        {
                            Debug.WriteLine("[WARN] Trade WebSocketState is not Open: " + _ws.State);
                            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                            onDisconnected?.Invoke();
                            return;
                        }
                        else
                        {
                            Debug.WriteLine($"[INFO] Trade WebSocket reconnecting {url}....");
                            try
                            {
                                _ws?.Dispose();
                                _ws = new ClientWebSocket();
                                await _ws.ConnectAsync(new Uri(url), cancellationToken);
                                if (_ws.State == WebSocketState.Open)
                                {
                                    onReconnected?.Invoke();
                                }
                                continue;
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    var segment = new ArraySegment<byte>(buffer);
                    var result = await _ws.ReceiveAsync(segment, cancellationToken);
                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Debug.WriteLine("[WARN] WebSocketMessageType.Close when recv msg.");
                        await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                        onDisconnected?.Invoke();
                        continue;
                    }

                    int count = result.Count;
                    while (!result.EndOfMessage)
                    {
                        if (count >= buffer.Length)
                        {
                            Debug.WriteLine("[ERROR] InvalidPayloadData.");
                            await _ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", cancellationToken);
                            onDisconnected?.Invoke();
                            continue;
                        }

                        segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                        result = await _ws.ReceiveAsync(segment, cancellationToken);

                        if (result.MessageType == WebSocketMessageType.Close)
                        {
                            Debug.WriteLine("[WARN] WebSocketMessageType.Close when continue-recv msg.");
                            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                            onDisconnected?.Invoke();
                            continue;
                        }
                        count += result.Count;
                    }

                    var rspTxt = Encoding.UTF8.GetString(buffer, 0, count);
                    WsMarketMessageResponse resp = null;
                    try
                    {
                        resp = Helper.MyJsonUnmarshal<WsMarketMessageResponse>(rspTxt);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("[ERROR] Deserialize Resp err: " + ex.Message);
                    }

                    if (resp == null)
                        continue;

                    if (resp?.ReqID?.Length > 0)
                    {
                        if (_request_callback.ContainsKey(resp.ReqID))
                        {
                            var action = _request_callback[resp.ReqID];
                            _request_callback.Remove(resp.ReqID);
                            action?.Invoke(resp.Code, resp.Data);
                        }
                        else
                        {
                            Debug.WriteLine("[WARN] ignore non-exists-reqid resp << " + rspTxt);
                        }
                    }
                    else if (resp?.Subj?.Length > 0)
                    {
                        if (resp.Subj == "onWallet")
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.Wlt>(resp.Data);
                            if (msg != null)
                            {
                                onWallet?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == "onTrade")
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.TrdRec>(resp.Data);
                            if (msg != null)
                            {
                                onTrade?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == "onOrder")
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.Ord>(resp.Data);
                            if (msg != null)
                            {
                                onOrder?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == "onPosition")
                        {
                            // {"subj":"onPosition","data":{"ADLIdx":-0.1733476102,"AId":"112508701","FeeEst":0.000000201,"Lever":0,"MI":0,"MMnF":0.0000013399,"MgnISO":0,"PId":"01CZ54KHZ9VAGCNPDWHS2XQREC","PrzBr":5.3328399195,"PrzIni":3732.57,"PrzLiq":5.3594841359,"ROE":-0.0002480831,"RPNL":-0.0000002009,"Sym":"BTC.BTC","Sz":1,"UId":"1125087","UPNL":-0.0000000665,"Val":-0.0002679119,"WId":"112508701BTC"}}
                            var msg = Helper.MyJsonSafeToObj<Models.Position>(resp.Data);
                            if (msg != null)
                            {
                                onPosition?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == "onWltLog")
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.WltLog>(resp.Data);
                            if (msg != null)
                            {
                                onWltLog?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else
                        {
                            Debug.WriteLine("[WANR]: unknown subj: " + rspTxt);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("[ERROR] invalid msg from trade-ws-server: " + rspTxt);
                    }
                }
            },
            cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        /// <summary>
        /// 与服务器比对时间
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_TimeAsync(Action<int, long> cb, CancellationToken cancellationToken)
        {
            long localtm = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var req = new WsTradeMessageRequest("Time", localtm);
            await this.SendRequestAsync(req, (code, data) =>
            {
                // {"code":0,"data":{"data":"1545808556560","time":1545808556646},"rid":"14"}
                long delta = 0;
                if (code == 0)
                {
                    var obj = data as Newtonsoft.Json.Linq.JObject;
                    if (obj != null && obj.ContainsKey("time"))
                    {
                        var servertm = obj["time"].ToObject<long>();
                        delta = servertm - localtm;
                    }
                }
                cb(code, delta);
            },
            cancellationToken);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_LoginAsync(Action<int, object> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("Login",
                new Models.TradeRequestLoginArgs
                {
                    UserName = this._uname,
                    UserCred = this._apikey,
                    DeviceInfo = "gmex-api-dotnet sample"
                });
            await this.SendRequestAsync(req, (code, data) =>
            {
                // {"code":9010,"data":"用户名或密码错误","rid":"1"}
                // {"code":0,"data":{"UserId":"1125623","UserName":"hexiaoyuan@126.com"},"rid":"2"}
                if (code == 0)
                {
                    var obj = data as Newtonsoft.Json.Linq.JObject;
                    if (obj != null && obj.ContainsKey("UserId"))
                    {
                        this._uid = obj["UserId"].ToObject<string>();
                    }
                }
                cb(code, data);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询合约/交易对列表详情
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetAssetDAsync(Action<int, List<Models.AssetD>> cb, CancellationToken cancellationToken)
        {
            // {"rid":"40","code":0,"data":[{Sym...},... ]}
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetAssetD", null);
            await this.SendRequestAsync(req, (code, data) =>
            {
                var instruments = new List<Models.AssetD>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        instruments.Add(l.ToObject<Models.AssetD>());
                    }
                }
                cb(code, instruments);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询交易对的扩展属性
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="Sym"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetAssetExAsync(int AccTyp, string Sym, Action<int, Models.AssetEx> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetAssetEx",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") }, { "Sym", Sym} }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                Models.AssetEx ex = null;
                var obj = data as Newtonsoft.Json.Linq.JObject;
                if (obj!=null)
                    ex = obj.ToObject<Models.AssetEx>();
                cb(code, ex);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询钱包信息
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetWalletsAsync(int AccTyp, Action<int, List<Models.Wlt>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetWallets",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var wlts = new List<Models.Wlt>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        wlts.Add(l.ToObject<Models.Wlt>());
                    }
                }
                cb(code, wlts);
            },
            cancellationToken);
        }


        /// <summary>
        /// 查询成交记录
        /// NOTE：这里只提供最近100条记录，更多信息请去网站下载。
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetTradesAsync(int AccTyp, Action<int, List<Models.TrdRec>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetTrades",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msgs = new List<Models.TrdRec>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        msgs.Add(l.ToObject<Models.TrdRec>());
                    }
                }
                cb(code, msgs);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询当前有效的报单
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetOrdersAsync(int AccTyp, Action<int, List<Models.Ord>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetOrders",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msgs = new List<Models.Ord>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        msgs.Add(l.ToObject<Models.Ord>());
                    }
                }
                cb(code, msgs);
            },
            cancellationToken);
        }


        /// <summary>
        /// 查询持仓
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetPositionsAsync(int AccTyp, Action<int, List<Models.Position>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetPositions",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msgs = new List<Models.Position>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        msgs.Add(l.ToObject<Models.Position>());
                    }
                }
                cb(code, msgs);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询钱包日志记录
        /// NOTE：这里只提供最近100条记录，更多信息请去网站下载。
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetWalletsLogAsync(int AccTyp, Action<int, List<Models.WltLog>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetWalletsLog",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msgs = new List<Models.WltLog>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        msgs.Add(l.ToObject<Models.WltLog>());
                    }
                }
                cb(code, msgs);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询资金中心钱包信息
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetCcsWalletsAsync(Action<int, List<Models.CcsWallet>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetCcsWallets",null);
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msgs = new List<Models.CcsWallet>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        msgs.Add(l.ToObject<Models.CcsWallet>());
                    }
                }
                cb(code, msgs);
            },
            cancellationToken);
        }

        /// <summary>
        /// 查询最近的历史报单
        /// NOTE： 这里只提供最近100条记录，更多信息请去网站下载。
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetHistOrdersAsync(int AccTyp, Action<int, List<Models.Ord>> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetHistOrders",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msgs = new List<Models.Ord>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    foreach (var l in array)
                    {
                        msgs.Add(l.ToObject<Models.Ord>());
                    }
                }
                cb(code, msgs);
            },
            cancellationToken);
        }

        /// <summary>
        /// 获取风险限额
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="sym"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_GetRiskLimitAsync(int AccTyp, string sym, Action<int, Models.RiskLimit> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("GetRiskLimit",
                new Dictionary<string, string>() { { "AId", this._uid + AccTyp.ToString("D2") }, { "Sym", sym} }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                var rl = (data as Newtonsoft.Json.Linq.JToken)?.ToObject<Models.RiskLimit>();
                cb(code, rl);
            },
            cancellationToken);
        }

        /// <summary>
        /// 设置超时取消报单
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="sec"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_CancelAllAfterAsync(int AccTyp, int sec, Action<int, object> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("CancelAllAfter",
                new Dictionary<string, object>() { { "AId", this._uid + AccTyp.ToString("D2") }, { "Sec", sec } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                // TODO
                cb(code, data);
            },
            cancellationToken);
        }

        /// <summary>
        /// 调整杠杆
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="sym"></param>
        /// <param name="postionId"></param>
        /// <param name="param"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_PosLeverageAsync(int AccTyp, string sym, string postionId, decimal param, Action<int, object> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("PosLeverage",
                new Dictionary<string, object>() { { "AId", this._uid + AccTyp.ToString("D2") }, { "Sym", sym }, { "PId", postionId }, {"Param", param} }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                // TODO
                cb(code, data);
            },
            cancellationToken);
        }

        /// <summary>
        /// 调整保证金
        /// </summary>
        /// <param name="AccTyp"></param>
        /// <param name="sym"></param>
        /// <param name="postionId"></param>
        /// <param name="param"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_PosTransMgnAsync(int AccTyp, string sym, string postionId, decimal param, Action<int, object> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("PosTransMgn",
                new Dictionary<string, object>() { { "AId", this._uid + AccTyp.ToString("D2") }, { "Sym", sym }, { "PId", postionId }, { "Param", param } }
                );
            await this.SendRequestAsync(req, (code, data) =>
            {
                // TODO
                cb(code, data);
            },
            cancellationToken);
        }


        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_OrderNewAsync(Models.Ord ord, Action<int, object> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("OrderNew", ord);
            await this.SendRequestAsync(req, (code, data) =>
            {
                //if (code == 0)
                //{
                //    var res = (data as Newtonsoft.Json.Linq.JToken)?.ToObject<Models.Ord>();
                //}
                cb(code, data);
            },
            cancellationToken);
        }

        /// <summary>
        /// 撤单
        /// </summary>
        /// <param name="ord"></param>
        /// <param name="cb"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task REQ_OrderDelAsync(Models.Ord ord, Action<int, object> cb, CancellationToken cancellationToken)
        {
            var req = new Gmex.API.WS.WsTradeMessageRequest("OrderDel", ord);
            await this.SendRequestAsync(req, (code, data) =>
            {
                //if (code == 0)
                //{
                //    var res = (data as Newtonsoft.Json.Linq.JToken)?.ToObject<Models.Ord>();
                //}
                cb(code, data);
            },
            cancellationToken);
        }


    }

}
