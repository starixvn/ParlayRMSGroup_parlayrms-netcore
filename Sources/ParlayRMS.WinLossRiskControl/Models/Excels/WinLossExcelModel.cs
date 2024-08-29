using Epplus.Mapper.Annotations;

namespace ParlayRMS.WinLossRiskControl.Models.Excels;

public class WinLossExcelModel
{
    [Cell("A3")]
    public string Username { get; set; }

    [Cell("B3")]
    public int NoMember { get; set; }

    [Cell("C3")]
    public int BetCount { get; set; }

    [Cell("D3")]
    public decimal MemberWinLoss { get; set; }

    [Cell("E3")]
    public decimal SmaWinLoss { get; set; }

    [Cell("F3")]
    public decimal ExtraPT { get; set; }

    [Cell("G3")]
    public decimal Commission { get; set; }

    [Cell("H3")]
    public decimal RealIncome { get; set; }

    [Cell("I3")]
    public decimal RealTurnover { get; set; }

    [Cell("J3")]
    public decimal Margin { get; set; }
}