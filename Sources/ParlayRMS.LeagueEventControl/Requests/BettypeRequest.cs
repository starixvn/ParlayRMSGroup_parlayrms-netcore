using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using System.Collections.Generic;

namespace ParlayRMS.LeagueEventControl.Requests;

public class BettypeRequest : IRequest<IEnumerable<BettypeModel>>
{
	public int SportType { get; set; }
}