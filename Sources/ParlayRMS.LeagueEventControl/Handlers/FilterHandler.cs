using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class FilterHandler(FilterService service) : IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
    IRequestHandler<BettypeRequest, IEnumerable<BettypeModel>>
{
    public Task<IEnumerable<LeagueModel>> Handle(LeagueRequest request, CancellationToken cancellationToken) => service.GetLeagues(request);

	public Task<IEnumerable<BettypeModel>> Handle(BettypeRequest request, CancellationToken cancellationToken) => service.GetBettypes(request);
}