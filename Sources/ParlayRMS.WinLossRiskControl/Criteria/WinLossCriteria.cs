using System;
using Fanex.Data.Repository;

namespace ParlayRMS.WinLossRiskControl.Criteria
{
    public class WinLossCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string Company { get; set; }

        public string ParlayType { get; set; }

        public string Selection { get; set; }

        public string Sporttype { get; set; }

        public override string GetSettingKey() => "RMS_Rpt_Winloss_Risk_Control";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(FromDate.ToString()) && !string.IsNullOrWhiteSpace(ToDate.ToString());
    }
}