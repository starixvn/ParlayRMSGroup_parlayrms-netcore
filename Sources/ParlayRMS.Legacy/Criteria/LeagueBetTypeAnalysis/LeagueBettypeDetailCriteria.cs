using Fanex.Data.Repository;

namespace ParlayRMS.Legacy.Criteria.LeagueBetTypeAnalysis;

public class LeagueBettypeDetailCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public string ParlayType { get; set; }
    public string LeagueGroups { get; set; }
    public string Leagues { get; set; }
    public int Site { get; set; }
    public int Match { get; set; }
    public string BetTeam { get; set; }
    public int ReportBy { get; set; }
    public int SportType { get; set; }
    public int BetType { get; set; }
    public decimal? HouseWL { get; set; }
    public string UserName { get; set; }
    public string Selection { get; set; }
    public override string GetSettingKey() => "RMS_Rpt_LeagueBettype_Detail";
    public override bool IsValid() => true;
}