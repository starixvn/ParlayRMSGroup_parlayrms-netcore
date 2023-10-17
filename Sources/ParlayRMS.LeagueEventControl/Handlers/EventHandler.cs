using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class EventHandler : IRequestHandler<EventRequest, IEnumerable<EventGroupModel>>
{
	private readonly LeagueEventService leagueEventService;

	public EventHandler(LeagueEventService leagueEventService)
	{
		this.leagueEventService = leagueEventService;
	}

	public Task<IEnumerable<EventGroupModel>> Handle(EventRequest request, CancellationToken cancellationToken) => leagueEventService.GetEvents(request);
}