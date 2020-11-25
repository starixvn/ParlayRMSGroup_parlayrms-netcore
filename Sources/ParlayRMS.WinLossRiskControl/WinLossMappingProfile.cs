using AutoMapper;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;

namespace ParlayRMS.WinLossRiskControl
{
    public class WinLossMappingProfile : Profile 
    {
        public WinLossMappingProfile()
        {
            CreateMap<WinLossRequest, WinLossCriteria>();
            CreateMap<CompanyWinLossRequest, CompanyWinLossCriteria>();
            CreateMap<AgentWinLossRequest, AgentWinLossCriteria>();

            CreateMap<WinLossRequest, AgentWinLossExcelRequest>();
            CreateMap<WinLossModel, WinLossExcelModel>();
        }
        
    }
}