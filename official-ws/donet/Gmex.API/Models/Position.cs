using Newtonsoft.Json;

namespace Gmex.API.Models
{
    public class Position
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
        /// 持仓ID
        /// </summary>
        public string PId { get; set; }


        /// <summary>
        /// 仓位(正数为多仓，负数为空仓)
        /// </summary>
        public decimal Sz { get; set; }

        /// <summary>
        /// 开仓平均价格
        /// </summary>
        public decimal PrzIni { get; set; }

        /// <summary>
        /// 已实现盈亏
        /// </summary>
        public double RPNL { get; set; }

        /// <summary>
        /// 杠杆
        /// </summary>
        public double Lever { get; set; }

        /// <summary>
        /// 动态数据, 最大杠杆
        /// </summary>
        public double LeverMax { get; set; }

        /// <summary>
        /// 动态数据, 有效MMR
        /// </summary>
        public double MMR { get; set; }

        /// <summary>
        /// 动态数据, 有效MIR
        /// </summary>
        public double MIR { get; set; }

        /// <summary>
        /// 仓位保证金
        /// </summary>
        public decimal MgnISO { get; set; }

        /// <summary>
        /// 逐仓下的已实现盈亏
        /// </summary>
        public decimal PNLISO { get; set; }

        /// <summary>
        /// 计算值：价值,仓位现时的名义价值，受到标记价格价格的影响
        /// </summary>
        public decimal Val { get; set; }

        /// <summary>
        /// 维持保证金,被仓位使用并锁定的保证金。
        /// </summary>
        public decimal MMnF { get; set; }

        /// <summary>
        /// 开仓保证金
        /// </summary>
        public decimal MI { get; set; }

        /// <summary>
        /// 计算值：未实现盈亏 PNL==  Profit And Loss
        /// </summary>
        public decimal UPNL { get; set; }

        /// <summary>
        /// 计算值: 强平价格 亏光当前保证金的 (如果是多仓，并且标记价格低于PrzLiq,则会被强制平仓。/如果是空仓,并缺标记价格高于PrzLiq，则会被强制平仓
        /// </summary>
        public decimal PrzLiq { get; set; }

        /// <summary>
        /// 计算值: 破产价格 BandRuptcy
        /// </summary>
        public decimal PrzBr { get; set; }

        /// <summary>
        /// 预估的平仓费
        /// </summary>
        public decimal FeeEst { get; set; }


        /// <summary>
        /// ROE
        /// </summary>
        public double ROE { get; set; }


        /// <summary>
        /// ADL红绿灯
        /// </summary>
        public int ADLLight { get; set; }

        /// <summary>
        /// ADLIdx, 这个是用来排序ADL的
        /// </summary>
        public double ADLIdx { get; set; }
    }
}
