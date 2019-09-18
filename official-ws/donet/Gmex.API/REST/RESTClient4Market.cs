using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gmex.API.REST
{
    public class RESTClient4Market
    {
        private string m_url;

        public RESTClient4Market(string serverurl)
        {
            m_url = serverurl;
        }

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
                if (jobj!=null)
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
        
        public async Task<Models.ServerInfo> GetServerInfoAsync()
        {
            var url = $"{m_url}/ServerInfo";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Models.ServerInfo>(contentStr);
            }
        }

        public async Task<List<Models.AssetD>> GetAssetDAsync()
        {
            var results = new List<Models.AssetD>();
            var url = $"{m_url}/GetAssetD";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0 ){
                    var array = resp.Data as Newtonsoft.Json.Linq.JArray;
                    if(array!=null)
                    {
                        foreach(var item in array)
                        {
                            var row = item.ToObject<Models.AssetD>();
                            if(row!=null)
                            {
                                results.Add(row);
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException($"invalid response: code={resp?.Code}");
                }
            }
            return results;
        }

        public async Task<List<Models.AssetEx>> GetAssetExAsync()
        {
            var results = new List<Models.AssetEx>();
            var url = $"{m_url}/GetAssetEx";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    var array = resp.Data as Newtonsoft.Json.Linq.JArray;
                    if (array != null)
                    {
                        foreach (var item in array)
                        {
                            var row = item.ToObject<Models.AssetEx>();
                            if (row != null)
                            {
                                results.Add(row);
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException($"invalid response: code={resp?.Code}");
                }
            }
            return results;
        }

        public async Task<List<Models.MktCompositeIndexTick>> GetCompositeIndexAsync()
        {
            var results = new List<Models.MktCompositeIndexTick>();
            var url = $"{m_url}/GetCompositeIndex";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    var array = resp.Data as Newtonsoft.Json.Linq.JArray;
                    if (array != null)
                    {
                        foreach (var item in array)
                        {
                            var row = item.ToObject<Models.MktCompositeIndexTick>();
                            if (row != null)
                            {
                                results.Add(row);
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException($"invalid response: code={resp?.Code}");
                }
            }
            return results;
        }

        public async Task<Models.MktInstrumentTick> GetTickAsync(string symb)
        {
            var url = $"{m_url}/GetTick?sym={symb}";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<Models.MktInstrumentTick>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        public async Task<Models.MktCompositeIndexTick> GetIndexTickAsync(string indexName)
        {
            var url = $"{m_url}/GetIndexTick?idx={indexName}";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<Models.MktCompositeIndexTick>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        public async Task<List<Models.MktTradeItem>> GetTradesAsync(string symb)
        {
            var results = new List<Models.MktTradeItem>();
            var url = $"{m_url}/GetTrades?sym={symb}";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    var array = resp.Data as Newtonsoft.Json.Linq.JArray;
                    if (array != null)
                    {
                        foreach (var item in array)
                        {
                            var row = item.ToObject<Models.MktTradeItem>();
                            if (row != null)
                            {
                                results.Add(row);
                            }
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException($"invalid response: code={resp?.Code}");
                }
            }
            return results;
        }

        public async Task<Models.MktOrder20Result> GetOrd20Async(string indexName)
        {
            var url = $"{m_url}/GetOrd20?sym={indexName}";
            using (var httpClient = new HttpClient())
            {
                var res = await httpClient.GetAsync(url);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<Models.MktOrder20Result>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }

        public async Task<Models.MktQueryKLineHistoryResult> GetHistKLineAsync(string sym, Models.MktKLineType typ, int beginSec, int offset, int count)
        {
            var url = $"{m_url}/GetHistKLine";
            using (var httpClient = new HttpClient())
            {
                var args = new Models.MktQueryKLineHistoryRequestArgs();
                args.Sym = sym;
                args.Typ = typ;
                args.Sec = beginSec;
                args.Offset = offset;
                args.Count = count;

                var content = new StringContent(Helper.MyJsonMarshal(args), Encoding.UTF8, "application/json");

                var res = await httpClient.PostAsync(url, content);
                var contentStr = await res.Content.ReadAsStringAsync();
                var resp = Helper.MyJsonUnmarshal<RestResponse>(contentStr);
                if (resp != null && resp.Code == 0)
                {
                    return Helper.MyJsonSafeToObj<Models.MktQueryKLineHistoryResult>(resp.Data);
                }
                throw new InvalidOperationException($"invalid response: code={resp?.Code}");
            }
        }
    }
}
