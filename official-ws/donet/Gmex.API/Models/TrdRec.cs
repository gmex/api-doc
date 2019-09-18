using Newtonsoft.Json;
using System;

namespace Gmex.API.Models
{
    public class TrdRec
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty(Order = 1, PropertyName = "UId")]
        public string UId { get; set; }

        /// <summary>
        /// 账户Id
        /// </summary>
        [JsonProperty(Order = 2, PropertyName = "AId")]
        public string AId { get; set; }

        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 钱包ID
        /// </summary>
        public string WId { get; set; }

        /// <summary>
        /// 撮合ID
        /// </summary>
        public string MatchId { get; set; }

        /// <summary>
        /// 委托ID
        /// </summary>
        public string OrdId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal Sz { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Prz { get; set; }

        /// <summary>
        /// 手续费记录
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        /// 货币符号
        /// </summary>
        public string FeeCoin { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public OrderVia Via { get; set; }

        /// <summary>
        /// 本成交的价值
        /// </summary>
        public decimal GrossVal { get; set; }
    }
}
