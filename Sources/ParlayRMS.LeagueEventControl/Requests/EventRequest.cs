using MediatR;
using ParlayRMS.LeagueEventControl.Models;

namespace ParlayRMS.LeagueEventControl.Requests;

public class EventRequest : IRequest<IEnumerable<EventGroupModel>>
{
    public string Selection { get; set; }

    public string LeagueIds { get; set; }

    public string ParlayType { get; set; }

    public string Level { get; set; }

    public int LimitRowCount { get; set; }

    public int? CustomerClass { get; set; }

    public bool IsViewDetail { get; set; }

    public byte SportType { get; set; }

    public string BetTypes { get; set; }

    public string UserName { get; set; }
}