namespace ParlayRMS.Legacy.Criteria.Common
{
    using Fanex.Data.Repository;

    public class CurrencySel_Criteria : CriteriaBase
    {
        public override string GetSettingKey() => "RMS_CurrencySel";

        public override bool IsValid() => true;
    }
}