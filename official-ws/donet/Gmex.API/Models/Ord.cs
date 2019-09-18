using Newtonsoft.Json;
using System;

namespace Gmex.API.Models
{
    /// <summary>
    /// Ord 委托/报单
    /// 
    /// 重要说明：
    /// 
    /// 1. 用户通常在本地维护 【所有委托】表； 主键使用 COrdId;
    /// 2. 性能考虑，建议单独建立一个【历史委托】的表专门存放已经不活动的委托，这些委托数据是不会再变化了;
    /// 3. 收到委托的推送消息后，应该去【所有委托】找到委托，进行内容更新替换，如需要则移到【历史委托】里;
    /// 4. 所有委托通常会被分为三种：当前委托，条件委托，历史委托;
    /// 5. 尽管 Server 端是使用 OrdId 来作为 Ord 的主键的，但是由于websockets的异步通讯机制，用户在下单（新建委托）时只能
    ///    自己维护 COrdId 而要等待服务器返回对应的 OrdId；因此建议本地使用GUID来自己维护自己Ord的唯一性；故，本地保存的
    ///    所有委托的主键使用 COrdId 比较方便;
    /// 6. 特别注意的是，OrderNew 的返回结果并不能保证一定比 onOrder 消息到来的快，因此，在下单前，应该先本地保存 COrdId 入
    ///    表以便 onOrder 能正确识别;
    /// 7. 委托的有效性，首先看 ErrorCode 是否为 0，然后再看 Status;
    /// 8. 对于条件委托，一开始OType是条件类型，触发后会自动变成 Limit 或 Market 类型；如在历史单中想判断是否条件单，可借助 
    ///    StopPrz 来进一步判断。
    /// </summary>
    public class Ord
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
        [JsonProperty(Order = 3)]
        public string Sym { get; set; }

        /// <summary>
        /// 钱包ID
        /// </summary>
        [JsonProperty(Order = 4)]
        public string WId { get; set; }

        /// <summary>
        /// 服务器端为其分配的报单ID
        /// </summary>
        [JsonProperty(Order = 5)]
        public string OrdId { get; set; }

        /// <summary>
        /// 客户端为其分配的报单ID
        /// </summary>
        [JsonProperty(Order = 6)]
        public string COrdId { get; set; }

        /// <summary>
        /// 委单方向: 1=买, -1=卖
        /// </summary>
        [JsonProperty(Order = 7)]
        public Dir Dir { get; set; }

        /// <summary>
        /// 报价类型: 1:Limit(限价委单 ), 2: Market(市价委单,匹配后转限价), 3: StopMarket (市价止损);
        /// </summary>
        [JsonProperty(Order = 8)]
        public OfferType OType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [JsonProperty(Order = 9)]
        public decimal Prz { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [JsonProperty(Order = 10)]
        public decimal Qty { get; set; }


        /// <summary>
        /// 显示数量。如果为0,则显示全部Qty
        /// </summary>
        [JsonProperty(Order =11)]
        public decimal QtyDsp { get; set; }


        /// <summary>
        /// TimeInForce/生效时间设定: 一直有效=0,剩余撤销=1,全部成交或者全部取消=2; GoodTillCancel=0,ImmediateOrCancel=1,FillOrKill=2;
        /// </summary>
        [JsonProperty(Order = 12)]
        public TimeInForce Tif { get; set; }


        /// <summary>
        /// 委托标志/标志位, OrdFlag 按位设置有效位
        /// </summary>
        [JsonProperty(Order = 13)]
        public UInt32 OrdFlag { get; set; }

        public void OrdFlag__Set(OrdFlag flag)
        {
            this.OrdFlag |= (UInt32)flag;
        }
        public bool OrdFlag__IsSet(OrdFlag flag)
        {
            return (this.OrdFlag & (UInt32)flag) != 0;
        }


        /// <summary>
        /// 来源
        /// </summary>
        [JsonProperty(Order = 14)]
        public OrderVia Via { get; set; }


        /// <summary>
        /// 下单时间戳,单位:毫秒
        /// </summary>
        [JsonProperty(Order = 15)]
        public UInt64 At { get; set; }

        /// <summary>
        /// 更新时间戳,单位:毫秒
        /// </summary>
        [JsonProperty(Order = 16)]
        public Int64 Upd { get; set; }


        /// <summary>
        /// 有效期,单位:毫秒,绝对时间
        /// </summary>
        [JsonProperty(Order = 17)]
        public UInt64 Until { get; set; }


        /// <summary>
        /// 最大价格变动次数.（价格档位)
        /// </summary>
        [JsonProperty(Order = 18)]
        public Int32 PrzChg { get; set; }


        /// <summary>
        /// 判断依据
        /// </summary>
        [JsonProperty(Order = 19)]
        public StopBy StopBy { get; set; }

        /// <summary>
        /// 报单的状态
        /// </summary>
        [JsonProperty(Order = 20)]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 止损价格,止盈价格
        /// </summary>
        [JsonProperty(Order = 21)]
        public decimal StopPrz { get; set; }

        /// <summary>
        /// 追踪委托中，回调的比率. Reverse Ratio. 小数。
        /// </summary>
        [JsonProperty(Order = 22)]
        public double TraceRR { get; set; }

        /// <summary>
        /// 追踪的Min
        /// </summary>
        [JsonProperty(Order = 23)]
        public double TraceMin { get; set; }

        /// <summary>
        /// 追踪的Max
        /// </summary>
        [JsonProperty(Order = 24)]
        public double TraceMax { get; set; }


        /// <summary>
        /// 冻结金额
        /// </summary>
        [JsonProperty(Order = 25)]
        public decimal Frz { get; set; }


        /// <summary>
        /// 委托保证金 Mgn Initial + 佣金
        /// </summary>
        [JsonProperty(Order = 26)]
        public decimal MM { get; set; }

        /// <summary>
        /// 预估的手续费：按照手续费计算
        /// </summary>
        [JsonProperty(Order = 27)]
        public decimal FeeEst { get; set; }

        /// <summary>
        /// 预估的UPNL	.. Predicatee
        /// </summary>
        [JsonProperty(Order = 28)]
        public decimal UPNLEst { get; set; }


        /// <summary>
        /// 已成交的数量
        /// </summary>
        [JsonProperty(Order = 29)]
        public decimal QtyF { get; set; }

        /// <summary>
        /// 已成交的平均价格 Prz Filled
        /// </summary>
        [JsonProperty(Order = 30)]
        public decimal PrzF { get; set; }

        /// <summary>
        /// 错误代码
        /// </summary>
        [JsonProperty(Order = 31)]
        public ErrCode ErrCode { get; set; }

        /// <summary>
        /// 错误文本
        /// </summary>
        [JsonProperty(Order = 32)]
        public string ErrTxt { get; set; }
    }

    public enum Dir
    {
        BID = 1,    // Buy
        BUY = 1,    // 相同含义
        ASK = -1,   // Sell
        SELL = -1,	// 相同含义
    }

    public enum OfferType
    {
        OT_Invalid = 0,
        Limit = 1,          // 限价委单
        Market = 2,         // 市价委单,匹配后转限价
        StopLimit = 3,      // 限价止损/盈利
        StopMarket = 4,     // 市价止损/盈利
        TraceLimit = 5,     // 追踪 限价
        TraceMarket = 6,    // 追踪 市价
    }

    public enum TimeInForce
    {
        GoodTillCancel = 0,     // 一直有效
        FillAndKill = 1,        // 部分成交后剩余委托取消， 有时候称为 ImmediateOrCancel
        FillOrKill = 2,         // 如果不能全部成交则取消委托, 全部成交或者全部撤销
    }

    public enum OrdFlag
    {
        OF_INVALID = 0,         // 占位，无意义
        POSTONLY = 1,           // 如果委托会立即成交，则不发送此委托
        REDUCEONLY = 2,         // 只减仓 TODO 未QA，未开放
        CLOSEONTRIGGER = 4,     // 触发后平仓 TODO 目前未实现
        IF_GREATERTHAN = 8,     // 条件指定为 如果价格大于
        IF_LESSTHAN = 16,       // 条件指定为 如果价格低于
        TRACE_ACTIVE = 32,      // 是否已经激活
        TRACE_FIRE = 64,        // 是否已经生效
        TRACE_AT_MAX = 128,     // 跟踪最大值的回调.如果不设定本标志，则跟踪MIN值
    }

    public enum OrderVia
    {
        Web = 1,
        App = 2,
        Api = 3,
        Liquidate = 4,  // 平仓 Liquidate
        ADLEngine = 5,  // ADL 减仓操作
        Settlement = 6, // 结算
        Trade = 7,      // 交易
        Fee = 8,        // 手续费
        Depo = 9,       // 存钱
        Wdrw = 10,      // 取钱
        Funding = 11,   // Funding
    }

    public enum OrderStatus
    {
        OS_Invalid = 0,
        Queueing = 1,   // 正在排队
        Matching = 2,   // 有效
        PostFail = 3,   // 提交失败
        Executed = 4,   // 已执行
    }


    public enum StopBy // 条件委托触发的判据
    {
        PriceMark = 0,      //标记价格
        PriceLatest = 1,    //最新成交
        PriceIndex = 2,     //指数价格
    }
}
