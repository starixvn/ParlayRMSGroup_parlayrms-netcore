using Fanex.Data.Repository;

namespace ParlayRMS.ManageAlert.Criteria
{
    public class ViewDetailCriteria : CriteriaBase
    {
        public long AlertId { get; set; }

        public override string GetSettingKey() => "RMS_ManageAlert_AlertDetail";

        public override bool IsValid() => true;
    }
}
