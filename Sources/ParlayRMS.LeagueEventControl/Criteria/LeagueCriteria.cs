using Fanex.Data.Repository;

namespace ParlayRMS.LeagueEventControl.Criteria;

public class LeagueCriteria : CriteriaBase
{
	public byte SportType { get; set; }

	public override string GetSettingKey() => "RMS_ESport_LeagueSel";

	public override bool IsValid() => true;
}