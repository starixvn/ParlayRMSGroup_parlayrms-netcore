namespace ParlayRMS.LeagueEventControl.Models
{
    public class LeagueDataModel
    {
        public string LeagueName { get; set; }

        public decimal HomeTotalStake { get; set; }

        public int HomeTotalTicket { get; set; }

        public decimal AwayTotalStake { get; set; }

        public int AwayTotalTicket { get; set; }

        public decimal OtherTotalStake { get; set; }

        public int OtherTotalTicket { get; set; }
    }
}
