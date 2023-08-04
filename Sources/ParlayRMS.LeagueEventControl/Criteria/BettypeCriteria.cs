using Fanex.Data.Repository;

namespace ParlayRMS.LeagueEventControl.Criteria;

public class BettypeCriteria : CriteriaBase
{
	public int SportType { get; set; }

	public override string GetSettingKey() => "RMS_BettypeSel";

	public override bool IsValid() => true;
}