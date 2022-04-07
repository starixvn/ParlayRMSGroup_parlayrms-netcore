using Fanex.Data.Repository;

namespace ParlayRMS.LeagueEventControl.Criteria
{
    public class EventCriteria : CriteriaBase
    {
        public string Selection { get; set; }

        public string LeagueIds { get; set; }

        public string ParlayType { get; set; }

        public string Level { get; set; }

        public int LimitRowCount { get; set; }

        public int? CustomerClass { get; set; }

        public bool IsViewDetail { get; set; }

        public byte SportType { get; set; }

        public string BetTypes { get; set; }

        public string UserName { get; set; }

        public override string GetSettingKey() => "RMS_Monitor_EventPerformance_Summary";

        public override bool IsValid() => true;
    }
}
