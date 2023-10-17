using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class EventDetailHandler : IRequestHandler<EventDetailRequest, List<EventDetailModel>>
{
	private readonly LeagueEventService leagueEventService;

	public EventDetailHandler(LeagueEventService leagueEventService)
	{
		this.leagueEventService = leagueEventService;
	}

	public Task<List<EventDetailModel>> Handle(EventDetailRequest request, CancellationToken cancellationToken) => leagueEventService.GetEventDetail(request);
}