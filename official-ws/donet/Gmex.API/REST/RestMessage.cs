using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmex.API.REST
{
    public class RestTradeMessageRequest
    {
        /// <summary>
        /// request name
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "req")]
        public string Request { get; set; }

        /// <summary>
        /// request args, object
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "args")]
        public object Args { get; set; }

        /// <summary>
        /// request message expires millon-time
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "expires")]
        public long Expires { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [JsonProperty(Order = 4, PropertyName = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Apikey
        /// </summary>
        [JsonProperty(Order = 5, PropertyName = "apikey")]
        public string Apikey { get; set; }

        /// <summary>
        /// request mesage signature
        /// </summary>
        [JsonProperty(Order = 6, PropertyName = "signature")]
        public string Signature { get; set; }


        public RestTradeMessageRequest(string request, object args)
        {
            this.Request = request;
            this.Args = args;
        }

        /// <summary>
        /// 生成消息签名
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="ApiSecret"></param>
        public void Sign(string UserName, string ApiKey, string ApiSecret)
        {
            if (UserName == null || ApiSecret == null || ApiSecret.Length <= 0)
                return;

            this.Username = UserName;
            this.Apikey = ApiKey;
            this.Expires = DateTimeOffset.Now.ToUnixTimeMilliseconds() + GlobalDefine.MSG_REQ_EXPIRES_DEFAULT_TIMEOUT;

            var txt = this.Request + (this.Args == null ? "" : Helper.MyJsonMarshal(this.Args)) + this.Expires + ApiSecret;

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
        public string GetSignedTxt(string UserName, string ApiKey, string ApiSecret)
        {
            this.Sign(UserName, ApiKey, ApiSecret);
            var txt = Helper.MyJsonMarshal(this);
            //return Encoding.UTF8.GetBytes(txt);
            return txt;
        }
    }

    class RestResponse
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "data")]
        public object Data { get; set; }
    }
}
