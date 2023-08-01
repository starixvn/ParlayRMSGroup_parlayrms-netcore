using Fanex.Data.Repository;

namespace ParlayRMS.ManageAlert.Criteria
{
    public class ViewLogCriteria : CriteriaBase
    {
        public long AlertId { get; set; }

        public override string GetSettingKey() => "RMS_ManageAlert_ViewLog";

        public override bool IsValid() => true;
    }
}
