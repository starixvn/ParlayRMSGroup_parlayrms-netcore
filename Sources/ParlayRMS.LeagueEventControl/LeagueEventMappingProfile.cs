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
        }
    }
}
