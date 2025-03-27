namespace ParlayRMS.Legacy.ViewModel.Report;

public class LeagueBetTypeAnalysisDetailOptions
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string ParlayType { get; set; }

    public string Leagues { get; set; }

    public string LeagueGroups { get; set; }

    public int Site { get; set; }

    public int Match { get; set; }

    public string BetTeam { get; set; }

    public int ReportBy { get; set; }

    public int SportType { get; set; }

    public int BetType { get; set; }

    public decimal? HouseWL { get; set; }

    public string Title { get; set; }

    public string Combo { get; set; }

    public string UserName { get; set; }
}
