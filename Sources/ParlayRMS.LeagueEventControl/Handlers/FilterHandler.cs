using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class FilterHandler : IRequestHandler<LeagueRequest, IEnumerable<LeagueModel>>,
	IRequestHandler<BettypeRequest, IEnumerable<BettypeModel>>
{
	private readonly FilterService filterService;

	public FilterHandler(FilterService filterService)
	{
		this.filterService = filterService;
	}

	public Task<IEnumerable<LeagueModel>> Handle(LeagueRequest request, CancellationToken cancellationToken) => filterService.GetLeagues(request);

	public Task<IEnumerable<BettypeModel>> Handle(BettypeRequest request, CancellationToken cancellationToken) => filterService.GetBettypes(request);
}