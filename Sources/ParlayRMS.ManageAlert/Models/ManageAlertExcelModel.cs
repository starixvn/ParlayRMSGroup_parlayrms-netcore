using Epplus.Mapper.Annotations;

namespace ParlayRMS.ManageAlert.Models;

public class ManageAlertExcelModel
{
    [Cell("A2")]
    public int No { get; set; }

    [Cell("B2")]
    public DateTime AlertTime { get; set; }

    [Cell("C2")]
    public string? BetId { get; set; }

    [Cell("D2")]
    public string? AlertType { get; set; }

    [Cell("E2")]
    public string? SportType { get; set; }

    [Cell("F2")]
    public string? UserName { get; set; }

    [Cell("G2")]
    public string? SimilarTickets { get; set; }

    [Cell("H2")]
    public string? Selection { get; set; }

    [Cell("I2")]
    public decimal Odds { get; set; }

    [Cell("J2")]
    public string? TotalBets { get; set; }

    [Cell("K2")]
    public decimal MemberStake { get; set; }

    [Cell("L2")]
    public decimal PotentialWinning { get; set; }

    [Cell("M2")]
    public decimal CompanyLose { get; set; }

    [Cell("N2")]
    public string? CheckedBy { get; set; }

    [Cell("O2")]
    public string? Action { get; set; }
}