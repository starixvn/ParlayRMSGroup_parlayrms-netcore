namespace ParlayRMS.Legacy.DTO.BetDetails
{
    public class ParlayBetDetails
    {
        public string TicketDetail { get; set; }

        public string TicketCompare { get; set; }

        public IList<string> ComboTypes { get; set; }
    }
}
