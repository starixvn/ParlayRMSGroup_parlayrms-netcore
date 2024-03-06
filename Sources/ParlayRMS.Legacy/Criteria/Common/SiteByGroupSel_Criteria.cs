namespace ParlayRMS.Legacy.Criteria.Common;

using Fanex.Data.Repository;

public class SiteByGroupSel_Criteria : CriteriaBase
{
    public override string GetSettingKey() => "RMS_SiteByGroupSel";

    public override bool IsValid() => true;
}