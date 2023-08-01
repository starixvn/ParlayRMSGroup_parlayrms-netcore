using Fanex.Data.Repository;

namespace ParlayRMS.ManageAlert.Criteria
{
    public class TakeActionCriteria : INonQueryCommand
    {
        public long AlertId { get; set; }

        public int UserId { get; set; }

        public bool IsChecked { get; set; }

        public byte ActionId { get; set; }


        public string GetSettingKey() => "RMS_ManageAlert_TakeAction";

        public bool IsValid() => true;
    }
}
