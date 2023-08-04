using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using System.Collections.Generic;

namespace ParlayRMS.LeagueEventControl.Requests;

public class EventDetailRequest : IRequest<List<EventDetailModel>>
{
	public string Selection { get; set; }

	public string LeagueIds { get; set; }

	public string ParlayType { get; set; }

	public string Level { get; set; }

	public int? CustomerClass { get; set; }

	public byte SportType { get; set; }

	public string BetTypes { get; set; }

	public string UserName { get; set; }

	public int MatchId { get; set; }

	public byte BetChoice { get; set; }

	public int DetailBettype { get; set; }
}