using System.Collections.Generic;
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

            CreateMap<AgentWinLossExcelRequest, AgentWinLossRequest>();
            CreateMap<AgentWinLossModel, WinLossExcelModel>();
            CreateMap<List<WinLossModel>, List<WinLossExcelModel>>();
        }
    }
}