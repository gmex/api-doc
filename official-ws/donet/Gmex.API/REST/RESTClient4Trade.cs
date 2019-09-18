using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gmex.API.REST
{
    public class RESTClient4Trade
    {
        private string m_url;
        private string m_uname;
        private string m_apikey;
        private string m_apisecret;

        private string m_uid;

        /// <summary>
        /// 当前的UID
        /// NOTE: 用户需要手动调用GetUserInfoAsync得到先。
        /// </summary>
        /// <returns></returns>
        public string GetCurrentUId()
        {
            return m_uid;
        }

        /// <summary>
        /// RESTClient4Trade
        /// </summary>
        /// <param name="serverurl"></param>
        /// <param name="uname"></param>
        /// <param name="apiKey"></param>
        /// <param name="apiSecret"></param>
        public RESTClient4Trade(string serverurl, string uname, string apiKey, string apiSecret)
        {
            m_url = serverurl;
            m_uname = uname;
            m_apikey = apiKey;
            m_apisecret = apiSecret;
        }

        /// <summary>
        /// 本地和服务器的时间差，毫秒
        /// </summary>
        /// <returns></returns>
        public async Task<long> GetTimeAsync()
        {
            long delta = 0;
            long localtm = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            var url = $"{m_url}/Time?args={localtm}";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var jobj = JObject.Parse(contentStr);
                if (jobj != null)
                {
                    if (jobj.ContainsKey("time"))
                    {
                        var servertm = jobj["time"].ToObject<long>();
                        delta = servertm - localtm;
                    }
                }
            }
            return delta;
        }

        /// <summary>
        /// 服务器信息
        /// </summary>
        /// <returns></returns>
        public async Task<Models.ServerInfo> GetServerInfoAsync()
        {
            var url = $"{m_url}/ServerInfo";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new InvalidOperationException($"http failed: code={res.StatusCode}. {contentStr}");
                }
                return JsonConvert.DeserializeObject<Models.ServerInfo>(contentStr);
            }
        }

        /// <summary>
        /// 获取用户的UID信息
        /// 通常创建后要先调用这个来得到UID，后继大量操作都需要UID才能进行。
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetUserInfoAsync()
        {
            var url = $"{m_url}/req/GetUserInfo";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetUserInfo", null);
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                if (res.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new InvalidOperationException($"http failed: code={res.StatusCode}. {contentStr}");
                }
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    var dict = Helper.MyJsonSafeToObj<Dictionary<string, string>>(resp.Data);
                    if (dict.ContainsKey("UserID"))
                    {
                        this.m_uid = dict["UserID"];
                    }
                    return this.m_uid;
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查询交易对
        /// </summary>
        /// <param name="accType">账号类型，01为合约，02为现货。</param>
        /// <returns></returns>
        public async Task<List<Models.AssetD>> GetAssetDAsync(int accType)
        {
            var url = $"{m_url}/req/GetAssetD";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetAssetD",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") } }
                    );
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.AssetD>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查钱包
        /// </summary>
        /// <param name="accType"></param>
        /// <returns></returns>
        public async Task<List<Models.Wlt>> GetWalletsAsync(int accType)
        {
            var url = $"{m_url}/req/GetWallets";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetWallets",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") } }
                    );
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.Wlt>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查资金中心/我的钱包
        /// </summary>
        /// <returns></returns>
        public async Task<List<Models.CcsWallet>> GetCcsWalletsAsync()
        {
            var url = $"{m_url}/req/GetCcsWallets";
            using (var httpClient = new HttpClient())
            {
                //var args = new RestTradeMessageRequest("GetWallets", new Dictionary<string, object>() { { "AId", m_uid + "00" } });
                var args = new RestTradeMessageRequest("GetCcsWallets", null);
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.CcsWallet>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查用户自己的最近成交记录
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        public async Task<List<Models.TrdRec>> GetTradesAsync(int accType, string sym)
        {
            var url = $"{m_url}/req/GetTrades";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetTrades",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, { "Sym", sym }/*, { "Start", 0 }, { "Stop", 100 }*/ });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.TrdRec>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查用户当前有效的报单列表
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        public async Task<List<Models.Ord>> GetOrdersAsync(int accType, string sym)
        {
            var url = $"{m_url}/req/GetOrders";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetOrders",
                   new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, { "Sym", sym }/*, { "Start", 0 }, { "Stop", 100 }*/ });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.Ord>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查持仓
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        public async Task<List<Models.Position>> GetPositionsAsync(int accType, string sym)
        {
            var url = $"{m_url}/req/GetPositions";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetPositions",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, { "Sym", sym }/*, { "Start", 0 }, { "Stop", 100 }*/ });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.Position>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查历史报单
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        public async Task<List<Models.Ord>> GetHistOrdersAsync(int accType, string sym)
        {
            var url = $"{m_url}/req/GetHistOrders";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetHistOrders",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, { "Sym", sym }/*, { "Start", 0 }, { "Stop", 100 }*/ });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.Ord>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 根据ordid来查询一个报单的信息
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="ordid"></param>
        /// <returns></returns>
        public async Task<Models.Ord> GetOrderByIDAsync(int accType, string ordid)
        {
            var url = $"{m_url}/req/GetOrderByID";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetOrderByID", 
                    new Dictionary<string, object>() {{ "AId", m_uid + accType.ToString("D2") }, {"OrdId", ordid}});
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<Models.Ord>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查最近的钱包日志记录
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        public async Task<List<Models.WltLog>> GetWalletsLogAsync(int accType, string sym)
        {
            var url = $"{m_url}/req/GetWalletsLog";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetWalletsLog",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, { "Sym", sym } });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<List<Models.WltLog>>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 查用户的风险限额
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <returns></returns>
        public async Task<Models.RiskLimit> GetRiskLimitAsync(int accType, string sym)
        {
            var url = $"{m_url}/req/GetRiskLimit";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("GetRiskLimit",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, {"Sym", sym } });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<Models.RiskLimit>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="ord"></param>
        /// <returns></returns>
        public async Task<Models.Ord> OrderNewAsync(Models.Ord ord)
        {
            var url = $"{m_url}/req/OrderNew";
            using (var httpClient = new HttpClient())
            {
                //ord.AId = m_uid + accType.ToString("D2");
                var args = new RestTradeMessageRequest("OrderNew", ord);
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null /*&& resp.Code == 0*/)
                {
                    return Helper.MyJsonSafeToObj<Models.Ord>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 扯单
        /// </summary>
        /// <param name="ord"></param>
        /// <returns></returns>
        public async Task<Models.Ord> OrderDelAsync(Models.Ord ord)
        {
            var url = $"{m_url}/req/OrderDel";
            using (var httpClient = new HttpClient())
            {
                //ord.AId = m_uid + accType.ToString("D2");
                var args = new RestTradeMessageRequest("OrderDel", ord);
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null /*(&& resp.Code == 0*/)
                {
                    return Helper.MyJsonSafeToObj<Models.Ord>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 设置超时撤单
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sec"></param>
        /// <returns></returns>
        public async Task<int> CancelAllAfterAsync(int accType, int sec)
        {
            var url = $"{m_url}/req/CancelAllAfter";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("CancelAllAfter",
                    new Dictionary<string, object>() { { "AId", m_uid + accType.ToString("D2") }, { "Sec", sec } });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null)
                {
                    //var retinfo =  Helper.MyJsonSafeToObj<Dictionary<string,object>>(resp.Data);
                    // Code Name Msg
                    return resp.Code;
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 调整杠杆
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <param name="postionId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> PosLeverageAsync(int accType, string sym, string postionId, decimal param)
        {
            var url = $"{m_url}/req/PosLeverage";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("PosLeverage",
                    new Dictionary<string, object>() {
                        { "AId", m_uid + accType.ToString("D2") },
                        { "Sym", sym },
                        { "PId", postionId },
                        { "Param", param },
                    });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null)
                {
                    return resp.Code;
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        /// <summary>
        /// 调整保证金
        /// </summary>
        /// <param name="accType"></param>
        /// <param name="sym"></param>
        /// <param name="postionId"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<int> PosTransMgnAsync(int accType, string sym, string postionId, decimal param)
        {
            var url = $"{m_url}/req/PosTransMgn";
            using (var httpClient = new HttpClient())
            {
                var args = new RestTradeMessageRequest("PosTransMgn",
                    new Dictionary<string, object>() {
                        { "AId", m_uid + accType.ToString("D2") },
                        { "Sym", sym },
                        { "PId", postionId },
                        { "Param", param },
                    });
                var content = new StringContent(args.GetSignedTxt(m_uname, m_apikey, m_apisecret), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null)
                {
                    return resp.Code;
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }
    }
}
