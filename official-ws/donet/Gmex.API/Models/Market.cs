using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gmex.API.Models
{

    /// <summary>
    /// 行情里，订阅后推送的 [综合指数] 的tick行情消息。
    /// </summary>
    public class MktCompositeIndexTick
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// 最新价
        /// </summary>
        public decimal Prz { get; set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Sz { get; set; }

        /// <summary>
        /// 24小时初始价格
        /// </summary>
        public decimal Prz24 { get; set; }

        /// <summary>
        /// 24小时最高价
        /// </summary>
        public decimal High24 { get; set; }

        /// <summary>
        /// 24小时最低价
        /// </summary>
        public decimal Low24 { get; set; }

        /// <summary>
        /// 24小时成交量
        /// </summary>
        public decimal Volume24 { get; set; }

        /// <summary>
        /// 24小时总成交额
        /// </summary>
        public decimal Turnover24 { get; set; }

        /// <summary>
        /// 第三方参考数据
        /// </summary>
        public object RefThirdParty { get; set; }
    }


    /// <summary>
    /// 行情里, 订阅后推送的 [交易对/合约] 的tick行情消息。
    /// </summary>
    public class MktInstrumentTick
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// 买1价
        /// </summary>
        public decimal PrzBid1 { get; set; }

        /// <summary>
        /// 买1量
        /// </summary>
        public decimal SzBid1 { get; set; }

        /// <summary>
        /// 总买量
        /// </summary>
        public decimal SzBid { get; set; }

        /// <summary>
        /// 卖1价
        /// </summary>
        public decimal PrzAsk1 { get; set; }

        /// <summary>
        /// 卖1量
        /// </summary>
        public decimal SzAsk1 { get; set; }

        /// <summary>
        /// 总卖量
        /// </summary>
        public decimal SzAsk { get; set; }


        /// <summary>
        /// 最新成交价
        /// </summary>
        public decimal LastPrz { get; set; }

        /// <summary>
        /// 最新标记价格
        /// </summary>
        public decimal SettPrz { get; set; }

        /// <summary>
        /// 24小时初始价格
        /// </summary>
        public decimal Prz24 { get; set; }

        /// <summary>
        /// 24小时最高价
        /// </summary>
        public decimal High24 { get; set; }

        /// <summary>
        /// 24小时最低价
        /// </summary>
        public decimal Low24 { get; set; }

        /// <summary>
        /// 24小时成交量
        /// </summary>
        public decimal Volume24 { get; set; }

        /// <summary>
        /// 24小时总成交额
        /// </summary>
        public decimal Turnover24 { get; set; }

        /// <summary>
        /// 总成交量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public decimal Turnover { get; set; }

        /// <summary>
        /// 总持仓量
        /// </summary>
        public decimal OpenInterest { get; set; }

        /// <summary>
        /// 多仓资金费率
        /// </summary>
        public decimal FundingLongR { get; set; }

        /// <summary>
        /// 空仓资金费率 -- 暂时没用
        /// </summary>
        public decimal FundingShortR { get; set; }

        /// <summary>
        /// 预测费率
        /// </summary>
        public decimal FundingPredictedR { get; set; }
    }


    /// <summary>
    /// 行情里，订阅全深度后，推送过来的一行的消息。
    /// 
    /// 重要说明：
    /// 1. 通常用户在本地内存中每个 Sym 都维护一套全深度行情表；
    /// 2. 一个 Sym 的全深度行情，使用 价格（Prz） 作为主键，数量（Sz）为值;
    /// 3. 收到推送的消息后，当 Sz 为 0 则删除此挡位，否则替换之;
    /// 4. 全深度数据在初始化时，可以通过 Begin(At == 0)/End(At == 1) 来判断已经盘口完整;
    /// 
    /// </summary>
    public class MktOrderItem
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Prz { get; set; }

        /// <summary>
        /// 方向; 1:bid, -1:ask
        /// </summary>
        public int Dir { get; set; }

        /// <summary>
        /// 量
        /// </summary>
        public decimal Sz { get; set; }
    }


    /// <summary>
    /// 行情里，订阅成交后，推送过来的消息。
    /// </summary>
    public class MktTradeItem
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Prz { get; set; }

        /// <summary>
        /// 方向; 1:bid, -1:ask
        /// </summary>
        public int Dir { get; set; }

        /// <summary>
        /// 量
        /// </summary>
        public decimal Sz { get; set; }

        /// <summary>
        /// 价值
        /// </summary>
        public decimal Val { get; set; }

        /// <summary>
        /// 撮合ID
        /// </summary>
        public string MatchID { get; set; }
    }


    /// <summary>
    /// KLine/K线/K柱 的类型
    /// 类型有: 1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 8h, 12h, 1d, 3d, 1w, 2w, 1M
    /// Kline/Candlestick chart intervals: m -> minutes; h -> hours; d -> days; w -> weeks; M -> months
    /// </summary>
    [DataContract]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MktKLineType
    {
        [EnumMember(Value = "invliad")]
        TYP_INVALID,
        /// 类型: 1m, 3m, 5m, 15m, 30m, 1h, 2h, 4h, 6h, 8h, 12h, 1d, 3d, 1w, 2w, 1M
        /// **
        /// 
        [EnumMember(Value = "1m")]
        TYP_1m,
        [EnumMember(Value = "3m")]
        TYP_3m,
        [EnumMember(Value = "5m")]
        TYP_5m,
        [EnumMember(Value = "15m")]
        TYP_15m,
        [EnumMember(Value = "30m")]
        TYP_30m,
        [EnumMember(Value = "1h")]
        TYP_1h,
        [EnumMember(Value = "2h")]
        TYP_2h,
        [EnumMember(Value = "4h")]
        TYP_4h,
        [EnumMember(Value = "6h")]
        TYP_6h,
        [EnumMember(Value = "8h")]
        TYP_8h,
        [EnumMember(Value = "12h")]
        TYP_12h,
        [EnumMember(Value = "1d")]
        TYP_1d,
        [EnumMember(Value = "3d")]
        TYP_3d,
        [EnumMember(Value = "1w")]
        TYP_1w,
        [EnumMember(Value = "2w")]
        TYP_2w,
        [EnumMember(Value = "1M")]
        TYP_1M
    }

    /// <summary>
    /// 行情里，一个 KLine 的数据结构。
    /// </summary>
    public class MktKLineItem
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MktKLineType Typ { get; set; }

        /// <summary>
        /// 时间戳,单位:秒
        /// </summary>
        public Int64 Sec { get; set; }

        /// <summary>
        /// 开始价
        /// </summary>
        public decimal PrzOpen { get; set; }

        /// <summary>
        /// 结束价
        /// </summary>
        public decimal PrzClose { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public decimal PrzHigh { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        public decimal PrzLow { get; set; }

        /// <summary>
        /// 总成交量
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public decimal Turnover { get; set; }
    }


    /// <summary>
    /// 行情里，订阅20档深度后，推送过来的消息。
    /// </summary>
    public class MktOrder20Result
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// Asks [][2]float64
        /// </summary>
        public List<List<double>> Asks { get; set; }

        /// <summary>
        /// Bids [][2]float64
        /// </summary>
        public List<List<double>> Bids { get; set; }

    }


    /// <summary>
    /// 行情里, 查询 KLine 历史数据时的请求参数
    /// </summary>
    public class MktQueryKLineHistoryRequestArgs
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MktKLineType Typ { get; set; }

        /// <summary>
        /// 开始的时间,单位:秒
        /// </summary>
        public int Sec { get; set; }

        /// <summary>
        /// 偏移量
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int Count { get; set; }
    }


    /// <summary>
    /// 行情里, 查询KLine返回的结果
    /// </summary>
    public class MktQueryKLineHistoryResult
    {
        /// <summary>
        /// 合约符合/交易对符号
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public MktKLineType Typ { get; set; }

        /// <summary>
        /// 返回结果的数量个数
        /// </summary>
        public Int64 Count { get; set; }

        /// <summary>
        /// 时间戳,单位:秒,数组
        /// </summary>
        public List<Int64> Sec { get; set; }

        /// <summary>
        /// 开始价
        /// </summary>
        public List<decimal> PrzOpen { get; set; }

        /// <summary>
        /// 结束价
        /// </summary>
        public List<decimal> PrzClose { get; set; }

        /// <summary>
        /// 最高价
        /// </summary>
        public List<decimal> PrzHigh { get; set; }

        /// <summary>
        /// 最低价
        /// </summary>
        public List<decimal> PrzLow { get; set; }

        /// <summary>
        /// 总成交量
        /// </summary>
        public List<decimal> Volume { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public List<decimal> Turnover { get; set; }
    }


    
}
