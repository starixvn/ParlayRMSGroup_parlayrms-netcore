using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using System.Collections.Generic;

namespace ParlayRMS.LeagueEventControl.Requests;

public class LeagueRequest : IRequest<IEnumerable<LeagueModel>>
{
	public byte SportType { get; set; }
}