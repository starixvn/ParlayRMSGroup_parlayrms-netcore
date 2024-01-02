using AutoMapper;
using MediatR;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class SimilarTicketDetailHandler(ManageAlertService alertService, IMapper mapper)
    : IRequestHandler<SimilarTicketDetailRequest, IEnumerable<SimilarTicketDetailModel>>
{
    public Task<IEnumerable<SimilarTicketDetailModel>> Handle(SimilarTicketDetailRequest request, CancellationToken cancellationToken)
	{
		var criteria = mapper.Map<ViewDetailCriteria>(request);
		return alertService.GetSimilarTicketDetail(criteria);
	}
}

public class PotentialWinningDetailHandler(ManageAlertService alertService, IMapper mapper)
    : IRequestHandler<PotentialWinningDetailRequest, IEnumerable<PotentialWinningDetailModel>>
{
    public Task<IEnumerable<PotentialWinningDetailModel>> Handle(PotentialWinningDetailRequest request,
		CancellationToken cancellationToken)
	{
		var criteria = mapper.Map<ViewDetailCriteria>(request);
		return alertService.GetPotentialWinningDetail(criteria);
	}
}