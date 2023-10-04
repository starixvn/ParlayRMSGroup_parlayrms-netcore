using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class LeagueEventHandler : IRequestHandler<LeagueDataRequest, IEnumerable<LeagueDataModel>>
{
	private readonly LeagueEventService leagueEventService;

	public LeagueEventHandler(LeagueEventService leagueEventService)
	{
		this.leagueEventService = leagueEventService;
	}

	public Task<IEnumerable<LeagueDataModel>> Handle(LeagueDataRequest request, CancellationToken cancellationToken) => leagueEventService.GetLeaguesData(request);
}