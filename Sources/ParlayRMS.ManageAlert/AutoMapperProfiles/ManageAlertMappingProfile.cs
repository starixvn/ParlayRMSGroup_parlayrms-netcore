using AutoMapper;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;

namespace ParlayRMS.ManageAlert.AutoMapperProfiles;

public class ManageAlertMappingProfile : Profile
{
    public ManageAlertMappingProfile()
    {
        CreateMap<ManageAlertRequest, ManageAlertCriteria>();
        CreateMap<ManageAlertExcelRequest, ManageAlertCriteria>();
        CreateMap<SimilarTicketDetailRequest, ViewDetailCriteria>();
        CreateMap<PotentialWinningDetailRequest, ViewDetailCriteria>();
        CreateMap<SimilarTicketDetailExcelRequest, ViewDetailCriteria>();
        CreateMap<PotentialWinningDetailExcelRequest, ViewDetailCriteria>();
        CreateMap<ViewLogRequest, ViewLogCriteria>();
        CreateMap<TakeActionRequest, TakeActionCriteria>();
        CreateMap<ManageAlertModel, ManageAlertExcelModel>();
        CreateMap<SimilarTicketDetailModel, SimilarTicketDetailExcelModel>();
        CreateMap<PotentialWinningDetailModel, PotentialWinningDetailExcelModel>();
    }
}