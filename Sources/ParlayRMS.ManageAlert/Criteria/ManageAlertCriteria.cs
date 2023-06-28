using Fanex.Data.Repository;

namespace ParlayRMS.ManageAlert.Criteria
{
    public class ManageAlertCriteria : CriteriaBase
    {
        public required string FromDate { get; set; }

        public required string ToDate { get; set; }

        public required string AlertType { get; set; }

        public string? Member { get; set; }

        public string? CheckBy { get; set; }

        public bool IsNoChecked { get; set; }

        public required string ActionType { get; set; }

        public required string TicketStatus { get; set; }
         
        public override string GetSettingKey() => "RMS_ManageAlert_Alert";

        public override bool IsValid() => true;
    }
}
