namespace Gmex.API.Models
{
    public enum ErrCode
    {
        NOERROR = 0,                // 没有错误
        GENERAL = 1,                // 数据错误
        DATA = 2,                   // 数据错误
        NOT_IMPLEMENTED = 3,        // 服务器未实现
        NO_MARGIN = 4,              // 保证金不足
        FATAL = 5,                  // 致命错误
        NOT_FOUND = 6,              // 未找到
        UNKNOWN_DIR = 7,            // 未知的委托方向
        INVALID_CODE = 8,           // 操作码错误
        EXISTS = 9,                 // 已存在
        NOT_FOUND_ORD = 10,         // 未找到委托
        PRZ_INVALID = 11,           // 价格错误
        EXPIRED = 12,               // 已过期
        NOT_SUFFICIENT = 13,        // 资金不足
        WILLFILL = 14,              // 对于PostOnly，本委托会成交
        EXECUTE_FAIL = 15,          // 对FillOrKill委托，这表示执行撮合失败
        UNUSED_______EXCEED_LIMIT_MINVAL = 16,  // 超过限制
        ORDQTY_TOO_BIG_TOO_SMALL = 17,          // 委托价值太小
        EXCEED_LIMIT_PRZ_QTY = 18,  // 价格或者数量超出限制
        DENYOPEN_BY_POS = 19,       // 仓位超出限制
        DENYOPEN_BY_RD = 20,        // 禁止开仓
        TRADE_STOPED = 21,          // 交易暂停
        EXCEED_PRZ_LIQ = 22,        // 超过强平价格
        TOO_MANY_ORDER = 23,        // 太多的委托
        DENYOPEN_BY_TIME = 24,      // 超出开仓时间限制
        MD5_INVALID = 25,           // MD5签名验证错误
        RATELIMIT = 26,             // 限速
        USER_CANCELED = 27,         // 用户撤销
        NOT_FOUND_WLT = 28,         // 无法找到钱包
        NOT_FOUND_MKT = 29,         // 未找到交易对
        EXCEED_MAXORDVAL = 30,      // 超过最大委托价值
        WILL_LIQUIDATE = 31,        // 将导致爆仓、强平
        NOT_IN_TRADE_PERIOD = 32,	// 非交易时间
        EXCEED_RAISE_FALL_R = 33,	// 超过涨跌停价格闲置
        PRZ_TOO_LOW = 34,			// 超出最小价格闲置
        EXCEED_TRADE_VOL = 35,		// 超出交易量限制
        EXCEED_TRADE_COUNT = 36,	// 超出交易次数限制
        EXCEED_ASK_BID_PRZ_RATE = 37, // 委托价格 超过盘口最新价格偏离
        NO_DEFAULT_RISKLIMIT = 64,  // 没有指定风险限额
    }
}
