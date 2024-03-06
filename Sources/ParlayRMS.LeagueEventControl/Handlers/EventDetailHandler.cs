using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class EventDetailHandler(LeagueEventService eventService)
    : IRequestHandler<EventDetailRequest, List<EventDetailModel>>
{
    public Task<List<EventDetailModel>> Handle(EventDetailRequest request, CancellationToken cancellationToken) => eventService.GetEventDetail(request);
}