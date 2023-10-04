using MediatR;
using ParlayRMS.LeagueEventControl.Models;

namespace ParlayRMS.LeagueEventControl.Requests;

public class LeagueRequest : IRequest<IEnumerable<LeagueModel>>
{
	public byte SportType { get; set; }
}