using Epplus.Mapper.Annotations;

namespace ParlayRMS.LeagueEventControl.Models.Excels
{
    public class MainTicketExcelModel
    {
        [Cell("A3")]
        public int No { get; set; }

        [Cell("B3")]
        public string TransDateString { get; set; }

        [Cell("C3")]
        public long TransId { get; set; }

        [Cell("D3")]
        public string Username { get; set; }

        [Cell("E3")]
        public string CustomerClass { get; set; }

        [Cell("F3")]
        public string Currency { get; set; }

        [Cell("G3")]
        public decimal Odds { get; set; }

        [Cell("H3")]
        public decimal Stake { get; set; }

        [Cell("I3")]
        public decimal PotentialWinningAmt { get; set; }

        [Cell("J3")]
        public decimal MaxPayOut { get; set; }

        [Cell("K3")]
        public int TotalBet { get; set; }

        [Cell("L3")]
        public int TotalSelection { get; set; }

        [Cell("M3")]
        public string SportTypeName { get; set; }

        [Cell("N3")]
        public string IP { get; set; }
    }
}
