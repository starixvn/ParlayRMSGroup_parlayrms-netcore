using AutoMapper;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Requests;

namespace ParlayRMS.Site
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<WinLossRequest, WinLossCriteria>();
            CreateMap<CompanyWinLossRequest, CompanyWinLossCriteria>();
            CreateMap<AgentWinLossRequest, AgentWinLossCriteria>();
        }
        
    }
}