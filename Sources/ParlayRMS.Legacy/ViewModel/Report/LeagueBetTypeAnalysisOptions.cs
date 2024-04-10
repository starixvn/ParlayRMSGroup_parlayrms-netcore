namespace ParlayRMS.Legacy.ViewModel.Report;

using System;
using Utilities;

public class LeagueBetTypeAnalysisOptions
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string ParlayType { get; set; }

    public string Leagues { get; set; }

    public string LeagueGroups { get; set; }

    public int Site { get; set; }

    public double Ratio { get; set; }

    public int ReportBy { get; set; }

    public int SportType { get; set; }

    public int BetType { get; set; }

    public decimal? HouseWL { get; set; }

    public string LeagueTitle { get; set; }

    public string BetTypeTitle { get; set; }

    public bool IsDetail { get; set; }

    public byte? SortById { get; set; }

    public bool IsSortAsc { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }

    public string GetTitle()
    {
        var title =
            $"{GetReportName()} from {FromDate:MMM dd, yyyy} to {ToDate:MMM dd, yyyy} - [{ViewLang.ParlayType}] {ModelsHelper.GetSimpleParlayTitle(ParlayType)} - [{ViewLang.Group}] - {ModelsHelper.GetLeagueGroupTitle(LeagueGroups)} - [{ViewLang.Site}] {ModelsHelper.GetSiteTitle(Site)}{GetLeagueOrBetTypeTitle()}";
        return title;
    }

    private string GetReportName() => ReportBy switch
    {
        1 => IsDetail ? "League Details Report" : "League Report",
        2 => IsDetail ? "BetType Details Report" : "BetType Report",
        _ => ""
    };

    private string GetLeagueOrBetTypeTitle() => ReportBy switch
    {
	    1 => IsDetail && !string.IsNullOrEmpty(LeagueTitle) ? $" - [{ViewLang.League}] {LeagueTitle}" : string.Empty,
	    2 => IsDetail && !string.IsNullOrEmpty(BetTypeTitle) ? $" - [{ViewLang.Bettype}] {BetTypeTitle}" : string.Empty,
		_ => ""
    };
}