using Fanex.Data.Repository;

namespace ParlayRMS.Legacy.Criteria.LeagueBetTypeAnalysis;

public class ParlayWonTicketsCriteria : CriteriaBase
{
	public string TransList { get; set; }
	public override string GetSettingKey() => "RMS_Rpt_LeagueBettype_Detail";
	public override bool IsValid() => true;
}
