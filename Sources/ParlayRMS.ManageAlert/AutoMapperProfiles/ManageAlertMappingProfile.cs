using AutoMapper;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Requests;

namespace ParlayRMS.ManageAlert.AutoMapperProfiles;

public class ManageAlertMappingProfile : Profile
{
	public ManageAlertMappingProfile()
	{
		CreateMap<ManageAlertRequest, ManageAlertCriteria>();
		CreateMap<SimilarTicketDetailRequest, ViewDetailCriteria>();
		CreateMap<PotentialWinningDetailRequest, ViewDetailCriteria>();
		CreateMap<ViewLogRequest, ViewLogCriteria>();
		CreateMap<TakeActionRequest, TakeActionCriteria>();
	}
}