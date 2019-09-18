using Newtonsoft.Json;

namespace Gmex.API.Models
{
    public class CcsWallet
    {
        /// <summary>
        /// 主键：资金账户id，uid+Wtype
        /// </summary>
        [JsonProperty(PropertyName = "wid")]
        public string WId { get; set; }


        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty(PropertyName = "uid")]
        public string UId { get; set; }

        /// <summary>
        /// 币种名称（如BTC/ETH等）
        /// </summary>
        [JsonProperty(PropertyName = "coin")]
        public string Coin { get; set; }

        /// <summary>
        /// 主账户余额
        /// </summary>
        [JsonProperty(PropertyName = "mainBal")]
        public string MainBal { get; set; }

        /// <summary>
        /// OTC法币账户余额
        /// </summary>
        [JsonProperty(PropertyName = "otcBal")]
        public string OtcBal { get; set; }

        /// <summary>
        /// 锁币额度
        /// </summary>
        [JsonProperty(PropertyName = "lockBal")]
        public string LockBal { get; set; }

        /// <summary>
        /// 理财额度
        /// </summary>
        [JsonProperty(PropertyName = "financeBal")]
        public string FinanceBal { get; set; }

        /// <summary>
        /// 质押额度
        /// </summary>
        [JsonProperty(PropertyName = "pawnBal")]
        public string PawnBal { get; set; }

        /// <summary>
        /// 欠贷款额度【负】
        /// </summary>
        [JsonProperty(PropertyName = "creditNum")]
        public string CreditNum { get; set; }

    }
}
