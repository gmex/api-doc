using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmex.API.WS
{
    public class WsTradeMessageRequest
    {
        /// <summary>
        /// request name
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "req")]
        public string Request { get; set; }

        /// <summary>
        /// request id, GUID
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "rid")]
        public string ReqID { get; set; }

        /// <summary>
        /// request args, object
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "args")]
        public object Args { get; set; }

        /// <summary>
        /// request message expires millon-time
        /// </summary>
        [JsonProperty(Order = 4, PropertyName = "expires")]
        public long Expires { get; set; }

        /// <summary>
        /// request mesage signature
        /// </summary>
        [JsonProperty(Order = 5, PropertyName = "signature")]
        public string Signature { get; set; }


        public WsTradeMessageRequest(string request, object args)
        {
            this.Request = request;
            this.Args = args;
            this.ReqID = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// 生成消息签名
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="ApiSecret"></param>
        public void Sign(string ApiKey, string ApiSecret)
        {
            if (ApiSecret==null || ApiSecret.Length <= 0)
                return;

            this.Expires = DateTimeOffset.Now.ToUnixTimeMilliseconds() + GlobalDefine.MSG_REQ_EXPIRES_DEFAULT_TIMEOUT;
            var txt = this.Request + this.ReqID + (this.Args==null ? "" : Helper.MyJsonMarshal(this.Args)) + this.Expires + ApiSecret;

            StringBuilder sb = new StringBuilder();
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(txt);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
            }
            this.Signature = sb.ToString();
        }

        /// <summary>
        /// 对签名后的消息体进行序列化并返回结果
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="ApiSecret"></param>
        /// <returns></returns>
        public byte[] GetSigneBuffer(string ApiKey, string ApiSecret)
        {
            this.Sign(ApiKey, ApiSecret);
            var txt = Helper.MyJsonMarshal(this);
            return Encoding.UTF8.GetBytes(txt);
        }
    }

    public class WsTradeMessageResponse
    {
        /// <summary>
        /// request id, GUID
        /// </summary>
        [JsonProperty(PropertyName = "rid")]
        public string ReqID { get; set; }

        /// <summary>
        /// request action result code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// subject name
        /// NOTE: 
        /// 服务器过来的消息有 rid 表示用户请求操作的结果，如果没有 rid 而有 subj，则表示
        /// 该消息为用户订阅的主题消息，具体 data 内容根据 subj 定义；
        /// 否则为无效消息。
        /// </summary>
        [JsonProperty(PropertyName = "subj")]
        public string Subj { get; set; }

        /// <summary>
        /// response data, object
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
    }

    public class WsMarketMessageRequest
    {
        /// <summary>
        /// request name
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "req")]
        public string Request { get; set; }

        /// <summary>
        /// request id, GUID
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "rid")]
        public string ReqID { get; set; }

        /// <summary>
        /// request args, object
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "args")]
        public object Args { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="ApiSecret"></param>
        /// <returns></returns>
        public byte[] GetMsgBuffer()
        {
            var txt = Helper.MyJsonMarshal(this);
            return Encoding.UTF8.GetBytes(txt);
        }

        public WsMarketMessageRequest(string request, object args)
        {
            this.Request = request;
            this.Args = args;
            this.ReqID = Guid.NewGuid().ToString();
        }
    }

    public class WsMarketMessageResponse
    {
        /// <summary>
        /// request id, GUID
        /// </summary>
        [JsonProperty(PropertyName = "rid")]
        public string ReqID { get; set; }

        /// <summary>
        /// request action result code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// subject name
        /// NOTE: 
        /// 行情服务器过来的消息有 rid 表示用户请求操作的结果，如果没有 rid 而有 subj，则表示
        /// 该消息为用户订阅的主题消息，具体 data 内容根据 subj 定义；
        /// 否则为无效消息。
        /// </summary>
        [JsonProperty(PropertyName = "subj")]
        public string Subj { get; set; }

        /// <summary>
        /// response data, object
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
    }
}
