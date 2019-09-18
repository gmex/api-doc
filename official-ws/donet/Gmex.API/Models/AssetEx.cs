using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gmex.API.Models
{
    public class AssetEx
    {
        /// <summary>
        /// 合约符合/交易对符号，如 BTC.USDT, BTC.BTC, BTC1903, BTC/USDT
        /// </summary>
        [JsonProperty(Order = -1, PropertyName = "Sym")]
        public string Sym { get; set; }

        /// <summary>
        /// 手续费计费方法
        /// </summary>
        public long FM { get; set; }

        /// <summary>
        /// 手续费，货币符号，如果未指定，则现货：按照收入额进行收取。期货：按照SettleCoin进行。
        /// 如果指定了FeeCoin则从该币种钱包内进行扣除。注意到，如果该钱包余额不足，则依旧使用SettleCoin进行
        /// </summary>
        public string FeeCoin { get; set; }

        /// <summary>
        /// 折扣率
        /// </summary>
        public decimal FeeDiscR { get; set; }

        /// <summary>
        /// 开放交易时间 (日内,毫秒)
        /// </summary>
        public UInt64 OnAt { get; set; }

        /// <summary>
        /// 关闭交易时间 (日内,毫秒)
        /// </summary>
        public UInt64 OffAt { get; set; }

        /// <summary>
        /// 价格涨价幅度 万分比 * 10000
        /// </summary>
        public Int64 RiseR { get; set; }

        /// <summary>
        /// 价格跌价幅度 万分比 * 10000
        /// </summary>
        public Int64 FallR { get; set; }

        /// <summary>
        /// 最小价格
        /// </summary>
        public decimal PrzMin { get; set; }

        /// <summary>
        /// 买入量
        /// </summary>
        public decimal LmtBid { get; set; }

        /// <summary>
        /// 卖出量
        /// </summary>
        public decimal LmtAsk { get; set; }

        /// <summary>
        /// 买入卖出总量
        /// </summary>
        public decimal LmtBidAsk { get; set; }

        /// <summary>
        /// 买入次数
        /// </summary>
        public UInt64 LmtNumBid { get; set; }

        /// <summary>
        /// 卖出次数
        /// </summary>
        public UInt64 LmtNumAsk { get; set; }

        /// <summary>
        /// 委托的买价偏离盘口比例(小数)
        /// </summary>
        public decimal BidPrzR { get; set; }

        /// <summary>
        /// 委托的卖价偏离盘口比例(小数)
        /// </summary>
        public decimal AskPrzR { get; set; }

        /// <summary>
        /// 买入卖出总次数
        /// </summary>
        public UInt64 LmtNumBidAsk { get; set; }

        /// <summary>
        /// 从0点开始，在每天的什么时间，开始重置统计值(绝对时间,毫秒)
        /// </summary>
        public UInt64 SumAt { get; set; }

        /// <summary>
        /// 重置间隔
        /// </summary>
        public UInt64 SumInterval { get; set; }

        /// <summary>
        /// 下次重制
        /// </summary>
        public UInt64 SumResetNext { get; set; }
    }
}
