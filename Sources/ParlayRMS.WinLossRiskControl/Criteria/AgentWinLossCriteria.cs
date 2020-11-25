using System;
using Fanex.Data.Repository;

namespace ParlayRMS.WinLossRiskControl.Criteria
{
    public class AgentWinLossCriteria : CriteriaBase
    {
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int CustId { get; set; }

        public int RoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalRecords { get; set; }

        public string ParlayType { get; set; }

        public string Sporttype { get; set; }

        public override string GetSettingKey() => "RMS_Rpt_Winloss_Risk_Control_Agent";

        public override bool IsValid() => !string.IsNullOrWhiteSpace(FromDate.ToString()) && !string.IsNullOrWhiteSpace(ToDate.ToString());
    }
}