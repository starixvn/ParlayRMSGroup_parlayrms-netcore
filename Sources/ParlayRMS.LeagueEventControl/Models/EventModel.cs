using System.Collections.Generic;

namespace ParlayRMS.LeagueEventControl.Models;

public class EventModel
{
	public int MatchId { get; set; }

	public string HomeName { get; set; }

	public string Market { get; set; }

	public decimal HomeAvgOdds { get; set; }

	public decimal HomeTotalStake { get; set; }

	public int HomeTotalTicket { get; set; }

	public string AwayName { get; set; }

	public decimal AwayAvgOdds { get; set; }

	public decimal AwayTotalStake { get; set; }

	public int AwayTotalTicket { get; set; }

	public bool IsOther { get; set; }

	public string AwayMarket { get; set; }

	public int Bettype { get; set; }

	public int AwayBettype { get; set; }

	public string LeagueName { get; set; }

	public string Event { get; set; }
}

public class EventGroupModel
{
	public int MatchId { get; set; }

	public List<EventModel> EventList { get; set; }

	public int FixedBettypes { get; set; }

	public int RowsTotal { get; set; }

	public decimal TotalStake { get; set; }
}

public class AwayEventModel
{
	public decimal AwayAvgOdds { get; set; }

	public decimal AwayTotalStake { get; set; }

	public int AwayTotalTicket { get; set; }

	public string AwayMarket { get; set; }

	public int AwayBettype { get; set; }
}