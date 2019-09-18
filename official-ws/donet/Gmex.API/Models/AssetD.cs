using Newtonsoft.Json;
using System;

namespace Gmex.API.Models
{
    public class AssetD
    {
        /// <summary>
        /// 合约符合/交易对符号，如 BTC.USDT, BTC.BTC, BTC1903, BTC/USDT
        /// </summary>
        [JsonProperty(Order = -1, PropertyName = "Sym")]
        public string Sym { get; set; }

        /// <summary>
        /// 开始时间,毫秒
        /// </summary>
        [JsonProperty(PropertyName = "Beg")]
        public long Beg { get; set; }

        /// <summary>
        /// 到期时间,毫秒
        /// </summary>
        public long Expire { get; set; }

        /// <summary>
        /// 到期前禁止开仓时间,毫秒
        /// </summary>
        public long DenyOpenAfter { get; set; }

        /// <summary>
        /// 市价委托的撮合的最多次数
        /// </summary>
        public Int32 PrzMaxChg { get; set; }

        /// <summary>
        /// 最小的价格变化
        /// </summary>
        public decimal PrzMinInc { get; set; }

        /// <summary>
        /// 最大委托价格
        /// </summary>
        public decimal PrzMax { get; set; }

        /// <summary>
        /// 最大委托数量
        /// </summary>
        public decimal OrderMaxQty { get; set; }

        /// <summary>
        /// 委托的最小数量
        /// </summary>
        public decimal OrderMinQty { get; set; }

        /// <summary>
        /// 最小合约数量,当前只支持为1;
        /// </summary>
        public decimal LotSz { get; set; }
       
        /// <summary>
        /// 标记价格
        /// </summary>
        public decimal PrzM { get; set; }

        /// <summary>
        /// 最新成交价格
        /// </summary>
        public decimal PrzLatest { get; set; }

        /// <summary>
        /// 指数价格
        /// </summary>
        public decimal PrzIndex { get; set; }

        /// <summary>
        /// 起始保证金率
        /// </summary>
        public decimal MIR { get; set; }

        /// <summary>
        /// 维持保证金率
        /// </summary>
        public decimal MMR { get; set; }

        /// <summary>
        /// 总持仓量
        /// </summary>
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public decimal Turnover { get; set; }

        /// <summary>
        /// 个人持仓比例激活条件,个人持仓数量.
        /// </summary>
        public long PosLmtStart { get; set; }

        /// <summary>
        ///  当前涨跌价格范围 Prz Rise Fall Range
        /// </summary>
        public decimal PrzRFMin { get; set; }

        /// <summary>
        /// 当前涨跌价格范围最大值
        /// </summary>
        public decimal PrzRFMax { get; set; }

        /// <summary>
        /// 提供流动性的费率
        /// </summary>
        public decimal FeeMkrR { get; set; }

        /// <summary>
        /// 消耗流动性的费率
        /// </summary>
        public decimal FeeTkrR { get; set; }

        /// <summary>
        /// 乘数
        /// </summary>
        public decimal Mult { get; set; }

        /// <summary>
        /// 从什么货币
        /// </summary>
        public string FromC { get; set; }

        /// <summary>
        /// 兑换为什么货币
        /// </summary>
        public string ToC { get; set; }

        /// <summary>
        /// 交易类型, 1-现货交易, 2-期货交易, 3-永续
        /// </summary>
        public Int32 TrdCls { get; set; }

        /// <summary>
        /// 合约、交易对的状态: 1-正常运行, 2-自动减仓, 3-暂停, 4-交易对已经关闭
        /// </summary>
        public Int32 MkSt { get; set; }

        /// <summary>
        /// 合约标志, 位操作: 1-反向报价or正向报价, 2-TODO, 4-自动结算, 8-禁止开仓, 16-停止交易
        /// </summary>
        public Int32 Flag { get; set; }

        /// <summary>
        /// 结算货币
        /// </summary>
        public string SettleCoin { get; set; }

        /// <summary>
        /// 报价货币
        /// </summary>
        public string QuoteCoin { get; set; }

        /// <summary>
        /// 结算费率
        /// </summary>
        public decimal SettleR { get; set; }

        /// <summary>
        /// 资金费率-多仓资金费率
        /// </summary>
        public decimal FundingLongR { get; set; }

        /// <summary>
        /// 资金费率-空仓资金费率
        /// </summary>
        public decimal FundingShortR { get; set; }

        /// <summary>
        /// 资金费率-预测费率
        /// </summary>
        public decimal FundingPredictedR { get; set; }

        /// <summary>
        /// 资金费用收取间隔 秒。每 8 小时 //秒，8小时 = 8 * 3600 秒 = 28800 秒 = 28800000 毫秒
        /// </summary>
        public int FundingInterval { get; set; }

        /// <summary>
        /// 下一个资金费率结算的时间。2018年4月16日 下午8:00:00. 时间戳 毫秒
        /// </summary>
        public long FundingNext { get; set; }

        /// <summary>
        /// 每日0点后的 FundingOffset 毫秒后 为第一个结算时间点.
        /// </summary>
        public long FundingOffset { get; set; }

        /// <summary>
        /// 资金费率计算参数: 公差
        /// </summary>
        public decimal FundingTolerance { get; set; }

        /// <summary>
        /// 资金费率结算佣金
        /// </summary>
        public decimal FundingFeeR { get; set; }

        /// <summary>
        /// 如果允许使用第三种货币支付手续费，则配置本项目
        /// </summary>
        public string FeeCoin { get; set; }

        /// <summary>
        /// 如果允许使用第三种货币支付手续费，这里配置折扣率
        /// </summary>
        public decimal FeeDiscR { get; set; }

        /// <summary>
        /// 交易对所属的分组ID，仅仅是一个逻辑分组概念.
        /// </summary>
        public long Grp { get; set; }
    }
}
