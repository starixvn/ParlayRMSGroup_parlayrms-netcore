using AutoMapper;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Models.Excels;
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
            CreateMap<EventDetailRequest, EventDetailCriteria>();
            CreateMap<ExcelRequest, EventDetailCriteria>();
            CreateMap<MainTicketModel, MainTicketExcelModel>();
        }
    }
}
