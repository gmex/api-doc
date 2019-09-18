using Newtonsoft.Json;
using System;

namespace Gmex.API.Models
{
    public class WltLog
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
        /// 货币类型
        /// </summary>
        public string Coin { get; set; }

        /// <summary>
        /// 钱包ID
        /// </summary>
        public string WId { get; set; }

        /// <summary>
        ///数量
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        ///手续费
        /// </summary>
        public decimal Fee { get; set; }

        /// <summary>
        ///货币地址(假设是出金，则是地址)
        /// </summary>
        public string Peer { get; set; }


        /// <summary>
        ///钱包结余
        /// </summary>
        public decimal WalBal { get; set; }

        /// <summary>
        /// 时间戳,单位:毫秒
        /// </summary>
        public Int64 At { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public WltOp Op { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public OrderVia Via { get; set; }

        /// <summary>
        /// Info
        /// </summary>
        public string Info { get; set; }


        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrCode { get; set; }

        /// <summary>
        /// 错误文本
        /// </summary>
        public string ErrTxt { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public OrderStatus Stat { get; set; }
    }


    public enum WltOp
    {
        WOP_INVALID = 0,
        DEPOSIT = 1,        // 存钱
        WITHDRAW = 2,       // 取钱
        PNL = 3,            // 已实现盈亏
        SPOT = 4,           // 现货交易
        TRAN_1_TO_MANY = 5, // 一账户 与 多账户 进行操作
        PNLISO = 6,         // 逐仓 已实现盈亏
        GIFT = 7,           // 礼金
    }
}
