using Newtonsoft.Json;

namespace Gmex.API.Models
{
    public class TradeRequestLoginArgs
    {

        /// <summary>
        /// 用户注册的 email 地址.
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 认证凭证, API_KEY, 字符串.
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "UserCred")]
        public string UserCred { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        [JsonProperty(Order = 3, PropertyName = "DeviceInfo")]
        public string DeviceInfo { get; set; }
    }
}
