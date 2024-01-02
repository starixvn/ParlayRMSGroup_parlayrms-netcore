using MediatR;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.Handlers;

public class LeagueEventHandler(LeagueEventService eventService)
    : IRequestHandler<LeagueDataRequest, IEnumerable<LeagueDataModel>>
{
    public Task<IEnumerable<LeagueDataModel>> Handle(LeagueDataRequest request, CancellationToken cancellationToken) => eventService.GetLeaguesData(request);
}