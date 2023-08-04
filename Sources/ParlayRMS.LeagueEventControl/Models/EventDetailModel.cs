namespace ParlayRMS.LeagueEventControl.Models;

public class EventDetailModel
{
	public string Market { get; set; }

	public decimal AvgOdds { get; set; }

	public decimal Stake { get; set; }

	public int Ticket { get; set; }

	public int Bettype { get; set; }
}