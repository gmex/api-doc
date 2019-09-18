using Newtonsoft.Json;
using System;

namespace Gmex.API.Models
{
    /// <summary>
    /// 服务器信息
    /// </summary>
    public class ServerInfo
    {
        /// <summary>
        /// code
        /// </summary>
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        /// <summary>
        /// 服务器名称
        /// </summary>
        [JsonProperty(PropertyName = "server")]
        public string Server { get; set; }

        /// <summary>
        /// 服务器当前时间，毫秒
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public Int64 MSec { get; set; }


        /// <summary>
        /// git_branch
        /// </summary>
        [JsonProperty(PropertyName = "git_branch")]
        public string GitBranch { get; set; }

        /// <summary>
        /// git_hash
        /// </summary>
        [JsonProperty(PropertyName = "git_hash")]
        public string GitHash { get; set; }
    }
}
