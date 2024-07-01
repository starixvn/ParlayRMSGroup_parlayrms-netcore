using MediatR;
using ParlayRMS.LeagueEventControl.Models;

namespace ParlayRMS.LeagueEventControl.Requests;

public class BettypeRequest : IRequest<IEnumerable<BettypeModel>>
{
    public int SportType { get; set; }
}