using Newtonsoft.Json;

namespace Gmex.API.Models
{
    public class Wlt
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
        /// 入金金额
        /// </summary>
        public decimal Depo { get; set; }

        /// <summary>
        /// 出金金额
        /// </summary>
        public decimal WDrw { get; set; }

        /// <summary>
        /// 已实现盈亏
        /// </summary>
        public decimal PNL { get; set; }

        /// <summary>
        /// 未实现盈亏：根据持仓情况、标记价格 刷新， 统计值
        /// </summary>
        public decimal UPNL { get; set; }

        /// <summary>
        /// 冻结金额
        /// </summary>
        public decimal Frz { get; set; }


        /// <summary>
        /// 委托保证金 = 计算自已有委单 + 平仓佣金 + 开仓佣金 Mgn Initial
        /// </summary>
        public decimal MI { get; set; }

        /// <summary>
        /// 仓位保证金 + 平仓佣金 Mgn Maintaince
        /// </summary>
        public decimal MM { get; set; }

        /// <summary>
        /// 风险度, Risk Degree
        /// </summary>
        public double RD { get; set; }

        /// <summary>
        /// 可取余额, 定时刷新。
        /// </summary>
        public double Wdrawable { get; set; }

        /// <summary>
        /// 现货交易出入金。
        /// </summary>
        public decimal Spot { get; set; }

        /// <summary>
        /// 赠送金额 不允许取出。
        /// </summary>
        public decimal Gift { get; set; }

    }
}
