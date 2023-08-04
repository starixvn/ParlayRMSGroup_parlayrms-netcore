namespace ParlayRMS.LeagueEventControl.Models;

public class LeagueModel
{
	public int LeagueId { get; set; }

	public string LeagueName { get; set; }

	public bool Checked { get; set; } = true;
}