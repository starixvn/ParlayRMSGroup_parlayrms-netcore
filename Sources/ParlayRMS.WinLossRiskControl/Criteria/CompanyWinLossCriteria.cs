using Fanex.Data.Repository;

namespace ParlayRMS.WinLossRiskControl.Criteria;

public class CompanyWinLossCriteria : CriteriaBase
{
    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Company { get; set; }

    public bool IsDeposit { get; set; }

    public string ParlayType { get; set; }

    public string Selection { get; set; }

    public string Sporttype { get; set; }

    public byte LiveStatus { get; set; }

    public override string GetSettingKey() => "RMS_Rpt_Winloss_Risk_Control_Company";

    public override bool IsValid() => !string.IsNullOrWhiteSpace(FromDate.ToString()) && !string.IsNullOrWhiteSpace(ToDate.ToString());
}