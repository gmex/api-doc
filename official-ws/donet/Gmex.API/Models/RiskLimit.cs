namespace Gmex.API.Models
{
    /// <summary>
    /// 风险限额
    /// </summary>
    public class RiskLimit
    {
        /// <summary>
        /// Symbol 交易对
        /// </summary>
        public string Sym { get; set; }

        /// <summary>
        /// Base Risk Limit 当 Pos Val 小于 Base 的时候
        /// </summary>
        public decimal Base { get; set; }


        /// <summary>
        /// Base Maintenance Margin Val 小于 Base 的时候 MMR
        /// </summary>
        public decimal BaseMMR { get; set; }

        /// <summary>
        /// Initial Margin Val 小于 Base 的时候 MIR
        /// </summary>
        public decimal BaseMIR { get; set; }

        /// <summary>
        /// Step  StepS = math.Ceil((Val - Base)/Step) 表示递增次数
        /// </summary>
        public decimal Step { get; set; }

        /// <summary>
        /// StepM  每次递增的时候，MMR MIR 的增量
        /// </summary>
        public decimal StepMR { get; set; }

        /// <summary>
        /// 最大持仓
        /// </summary>
        public decimal PosSzMax { get; set; }

        /// <summary>
        /// 每次递增的时候，MIR 的增量
        /// </summary>
        public decimal StepIR { get; set; }

        /// <summary>
        /// 单笔委托的最大价值
        /// </summary>
        public decimal MaxOrdVal { get; set; }
    }

}
