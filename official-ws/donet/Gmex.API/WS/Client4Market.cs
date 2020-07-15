using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gmex.API.WS
{
    public class Client4Market
    {
        

        //private ConcurrentDictionary<string, Action<int, object>> _request_callback = new ConcurrentDictionary<string, Action<int, object>>();
        private Dictionary<string, Action<int, object>> _request_callback = new Dictionary<string, Action<int, object>>();

        private ClientWebSocket _ws;

        public Client4Market()
        {
            // TODO
        }

        public void SafeClose()
        {
            _ws?.Dispose();
            _ws = null;
        }

        public bool IsConnected()
        {
            return _ws != null && _ws.State == WebSocketState.Open;
        }

        //public Task ConnectAsync(string url, CancellationToken cancellationToken)
        //{
        //    this._ws?.Dispose();

        //    this._ws = new ClientWebSocket();
        //    return this._ws.ConnectAsync(new Uri(url), cancellationToken);
        //}

        private Task SendRequestAsync(WsMarketMessageRequest msg, Action<int, object> action, CancellationToken cancellationToken)
        {
            if (this._ws == null || this._ws.State != WebSocketState.Open)
            {
                throw new InvalidOperationException("websocket is not open");
            }

            try
            {
                _request_callback[msg.ReqID] = action; //_request_callback.TryAdd(msg.ReqID, action);
                return this._ws?.SendAsync(new ArraySegment<byte>(msg.GetMsgBuffer()), WebSocketMessageType.Text, true, cancellationToken);
            }
            catch (Exception ex)
            {
                _request_callback.Remove(msg.ReqID);
                throw ex;
            }
        }

        public async Task ReceiveLoop(string url, bool autoReconnect,
            Action<Models.MktTradeItem> onMktTradeItem,
            Action<Models.MktOrderItem> onMktOrderItem,
            Action<Models.MktOrder20Result> onMktOrder20Result,
            Action<Models.MktInstrumentTick> onMktInstrumentTick,
            Action<Models.MktCompositeIndexTick> onMktCompositeIndexTick,
            Action<Models.MktKLineItem> onMktKLineItem,
            Action onDisconnected,
            Action onReconnected,
            CancellationToken cancellationToken)
        {
            try
            {
                _ws?.Dispose();
                _ws = new ClientWebSocket();
                await _ws.ConnectAsync(new Uri(url), cancellationToken);
            }catch(Exception ex)
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
                        Debug.WriteLine($"[NOTE] Market WebSocket({url}) is Cancelled!");
                        await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                        onDisconnected?.Invoke();
                        return;
                    }

                    if (_ws.State != WebSocketState.Open)
                    {
                        if (!autoReconnect)
                        {
                            Debug.WriteLine("[WARN] Market WebSocketState is not Open: " + _ws.State);
                            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
                            onDisconnected?.Invoke();
                            return;
                        }
                        else
                        {
                            Debug.WriteLine($"[INFO] Market WebSocket reconnecting {url}....");
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
                        if (resp.Subj == K_SUBJ_TRADE)
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.MktTradeItem>(resp.Data);
                            if (msg != null)
                            {
                                onMktTradeItem?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == K_SUBJ_ORDERL2)
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.MktOrderItem>(resp.Data);
                            if (msg != null)
                            {
                                //if (msg.At == 0)
                                //    Debug.WriteLine($"[NOTE] {msg.Sym} orderl2 begin...");
                                //else if(msg.At==1)
                                //    Debug.WriteLine($"[NOTE] {msg.Sym} orderl2 end.");

                                onMktOrderItem?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == K_SUBJ_ORDER20)
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.MktOrder20Result>(resp.Data);
                            if (msg != null)
                            {
                                onMktOrder20Result?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == K_SUBJ_TICK)
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.MktInstrumentTick>(resp.Data);
                            if (msg != null)
                            {
                                onMktInstrumentTick?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == K_SUBJ_INDEX)
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.MktCompositeIndexTick>(resp.Data);
                            if (msg != null)
                            {
                                onMktCompositeIndexTick?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == K_SUBJ_KLINE)
                        {
                            var msg = Helper.MyJsonSafeToObj<Models.MktKLineItem>(resp.Data);
                            if (msg != null)
                            {
                                onMktKLineItem?.Invoke(msg);
                            }
                            else
                            {
                                Debug.WriteLine("[WARN] invalid resp << " + rspTxt);
                            }
                        }
                        else if (resp.Subj == K_SUBJ_NOTIF)
                        {
                            // 系统广播消息
                            // TODO
                            Debug.WriteLine("[NOTE] ignore notify msg << " + rspTxt);
                        }
                        else
                        {
                            Debug.WriteLine("[WANR]: unknown subj: " + rspTxt);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("[ERROR] invalid msg from market-ws-server: " + rspTxt);
                    }
                }
            }, 
            cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }



        public async Task REQ_TimeAsync(Action<int, long> cb, CancellationToken cancellationToken)
        {
            // {"req":"Time","rid":"123","expires":1545722275616,"args":1545722274616}
            // {"rid":"123","code":0,"data":{"time":1545722274605,"data":"1545722274616"}}

            long localtm = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var req = new WsMarketMessageRequest("Time", localtm);
            await this.SendRequestAsync(req, (code, data) =>
            {
                long delta = 0;
                if (code == 0)
                {
                    var obj = data as Newtonsoft.Json.Linq.JObject;
                    if (obj != null)
                    {
                        var servertm = obj["time"].ToObject<long>();
                        delta = servertm - localtm;
                    }
                }
                cb(code, delta);
            },
            cancellationToken);
        }

        public async Task REQ_GetCompositeIndexAsync(Action<int, List<string>> cb, CancellationToken cancellationToken)
        {
            // {"rid":"41","code":0,"data":["CI_ETH","CI_ETC","CI_LTC","CI_BTC","CI_EOS","CI_XRP"]}
            var req = new WsMarketMessageRequest("GetCompositeIndex", null);
            await this.SendRequestAsync(req, (code, data) =>
            {
                List<string> indices = new List<string>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    //var lines = data as Array;
                    foreach (var l in array)
                    {
                        indices.Add(l.ToObject<string>());
                    }
                }
                cb(code, indices);
            },
            cancellationToken);
        }

        public async Task REQ_GetAssetDAsync(Action<int, List<Models.AssetD>> cb, CancellationToken cancellationToken)
        {
            // {"rid":"40","code":0,"data":[{Sym...},... ]}
            var req = new Gmex.API.WS.WsMarketMessageRequest("GetAssetD", null);
            await this.SendRequestAsync(req, (code, data) =>
            {
                var instruments = new List<Models.AssetD>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    //var lines = data as Array;
                    foreach (var l in array)
                    {
                        instruments.Add(l.ToObject<Models.AssetD>());
                    }
                }
                cb(code, instruments);
            },
            cancellationToken);
        }

        public async Task REQ_GetAssetExAsync(Action<int, List<Models.AssetEx>> cb, CancellationToken cancellationToken)
        {
            // {"rid":"40","code":0,"data":[{Sym...},... ]}
            var req = new Gmex.API.WS.WsMarketMessageRequest("GetAssetEx", null);
            await this.SendRequestAsync(req, (code, data) =>
            {
                var instruments = new List<Models.AssetEx>();
                if (code == 0)
                {
                    var array = data as Newtonsoft.Json.Linq.JArray;
                    //var lines = data as Array;
                    foreach (var l in array)
                    {
                        instruments.Add(l.ToObject<Models.AssetEx>());
                    }
                }
                cb(code, instruments);
            },
            cancellationToken);
        }

        public async Task REQ_GetHistKLine(string sym, Models.MktKLineType typ, int beginSec, int offset, int count, Action<int, Models.MktQueryKLineHistoryResult> cb, CancellationToken cancellationToken)
        {
            var args = new Models.MktQueryKLineHistoryRequestArgs
            {
                Sym = sym,
                Typ = typ,
                Sec = beginSec,
                Offset = offset,
                Count = count
            };
            var req = new WsMarketMessageRequest("GetHistKLine", args);

            await this.SendRequestAsync(req, (code, data) =>
            {
                //Models.MxQueryKLineHistoryResult result = new Models.MxQueryKLineHistoryResult();
                if (code == 0)
                {
                    var res = Helper.MyJsonSafeToObj<Models.MktQueryKLineHistoryResult>(data);
                    cb(code, res);
                }
                else
                {
                    Debug.WriteLine("[-] GetHistKLine failed: " + data.ToString());
                    cb(code, null);
                }
            },
            cancellationToken);
        }
        public async Task REQ_Sub(List<string> subjects, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            var req = new WsMarketMessageRequest(unSub ? "UnSub" : "Sub", subjects);
            await this.SendRequestAsync(req, (code, data) =>
            {
                var msg = string.Empty;
                if (data != null)
                {
                    msg = data.ToString();
                }
                cb(code, msg);
            },
            cancellationToken);
        }


        /***
         * | 订阅内容 | 描述 |
         * |:------:|:------|
         * | TICK    | 比如: tick_BTC1812|
         * | 成交     | 比如: trade_BTC1812|
         * | 20档深度 | 比如: order20_BTC1812|
         * | 全档深度 | 比如: orderl2_BTC1812|
         * | K线      | 比如: kline_1m_BTC1812，kline_1h_BTC1812|
         * | 指数     | 比如: index_CI_BTC，index_CI_ETH|
         ***/

        public const string K_SUBJ_TICK = "tick";
        public const string K_SUBJ_TRADE = "trade";
        public const string K_SUBJ_ORDER20 = "order20";
        public const string K_SUBJ_ORDERL2 = "orderl2";
        public const string K_SUBJ_KLINE = "kline";
        public const string K_SUBJ_INDEX = "index";
        public const string K_SUBJ_NOTIF = "notification";

        public async Task REQ_Sub_tick_xxx(string sym, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            await REQ_Sub(new List<string> { K_SUBJ_TICK + "_" + sym }, cb, cancellationToken, unSub);
        }
        public async Task REQ_Sub_trade_xxx(string sym, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            await REQ_Sub(new List<string> { K_SUBJ_TRADE + "_" + sym }, cb, cancellationToken, unSub);
        }
        public async Task REQ_Sub_order20_xxx(string sym, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            await REQ_Sub(new List<string> { K_SUBJ_ORDER20 + "_" + sym }, cb, cancellationToken, unSub);
        }
        public async Task REQ_Sub_orderl2_xxx(string sym, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            await REQ_Sub(new List<string> { K_SUBJ_ORDERL2 + "_" + sym }, cb, cancellationToken, unSub);
        }
        public async Task REQ_Sub_kline_xxx(string sym, Models.MktKLineType typ, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            
            await REQ_Sub(new List<string> { K_SUBJ_KLINE + "_" + Gmex.API.Helper.GetEnumMemberValue(typ) + "_" + sym }, cb, cancellationToken, unSub);
        }
        public async Task REQ_Sub_index_xxx(string ci, Action<int, string> cb, CancellationToken cancellationToken, bool unSub)
        {
            await REQ_Sub(new List<string> { K_SUBJ_INDEX + "_" + ci }, cb, cancellationToken, unSub);
        }


        
    }
}
