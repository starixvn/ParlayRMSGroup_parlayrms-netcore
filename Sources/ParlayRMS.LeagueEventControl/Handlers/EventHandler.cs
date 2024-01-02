using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class EventHandler(LeagueEventService eventService)
    : IRequestHandler<EventRequest, IEnumerable<EventGroupModel>>
{
    public Task<IEnumerable<EventGroupModel>> Handle(EventRequest request, CancellationToken cancellationToken) => eventService.GetEvents(request);
}