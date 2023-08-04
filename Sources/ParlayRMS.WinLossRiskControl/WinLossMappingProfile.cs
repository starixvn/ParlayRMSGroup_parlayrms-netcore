using AutoMapper;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Models.Excels;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Requests.Excels;

namespace ParlayRMS.WinLossRiskControl;

public class WinLossMappingProfile : Profile
{
	public WinLossMappingProfile()
	{
		CreateMap<WinLossRequest, WinLossCriteria>();
		CreateMap<SuperWinLossRequest, CompanyWinLossCriteria>();
		CreateMap<AgentWinLossRequest, CompanyWinLossCriteria>();
		CreateMap<AgentWinLossRequest, AgentWinLossCriteria>();

		CreateMap<SiteWinLossExcelRequest, WinLossRequest>();
		CreateMap<SuperWinLossExcelRequest, SuperWinLossRequest>();
		CreateMap<AgentWinLossExcelRequest, AgentWinLossRequest>();
		CreateMap<MemberWinLossExcelRequest, AgentWinLossRequest>();
		CreateMap<WinLossModel, MemberWinLossExcelModel>();
		CreateMap<WinLossModel, WinLossExcelModel>();
	}
}