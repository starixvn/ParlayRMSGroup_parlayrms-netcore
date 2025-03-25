namespace ParlayRMS.Legacy.DTO.LeagueBetTypeAnalysis;

public class RMS_Rpt_LeagueBettype_Analysis
{
    public int No { get; set; }

    public string Event { get; set; }

    public string BetChoice { get; set; }

    public string TransList { get; set; }

    public int LeagueId { get; set; }

    public int BetTypeId { get; set; } = 0;

    public string BetType { get; set; }

    public int GroupId { get; set; }

    public string GroupName { get; set; }

    public string League { get; set; }

    public string LeagueName { get; set; }

    public int TotalTickets { get; set; }

    public int DrawTickets { get; set; }

    public string Ratio { get; set; }

    public int WonTickets { get; set; }

    public int ParlayWonTickets { get; set; }

    public decimal TurnOver { get; set; }

    public decimal MemberWinloss { get; set; }

    public decimal MainHouseWinloss { get; set; }

    public string BetTeam { get; set; }

    public int MatchId { get; set; }

    public int CustomerClass { get; set; }
}