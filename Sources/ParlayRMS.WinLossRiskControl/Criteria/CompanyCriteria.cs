using Fanex.Data.Repository;

namespace ParlayRMS.WinLossRiskControl.Criteria
{
    public class CompanyCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "RMS_SiteByGroupSel";

        public override bool IsValid() => true;
    }
}