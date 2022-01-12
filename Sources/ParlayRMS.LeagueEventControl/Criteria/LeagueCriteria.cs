using Fanex.Data.Repository;

namespace ParlayRMS.LeagueEventControl.Criteria
{
    public class LeagueCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "RMS_ESport_LeagueSel";

        public override bool IsValid() => true;
    }
}
