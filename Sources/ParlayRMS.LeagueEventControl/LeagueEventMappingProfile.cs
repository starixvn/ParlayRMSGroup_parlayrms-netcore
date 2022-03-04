using AutoMapper;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl
{
    public class LeagueEventMappingProfile : Profile
    {
        public LeagueEventMappingProfile()
        {
            CreateMap<LeagueDataRequest, LeagueDataCriteria>();
            CreateMap<EventRequest, EventCriteria>();
            CreateMap<LeagueRequest, LeagueCriteria>();
            CreateMap<BettypeRequest, BettypeCriteria>();
        }
    }
}
