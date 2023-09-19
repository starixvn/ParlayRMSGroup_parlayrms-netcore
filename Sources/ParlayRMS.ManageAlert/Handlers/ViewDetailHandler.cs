using AutoMapper;
using MediatR;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class SimilarTicketDetailHandler : IRequestHandler<SimilarTicketDetailRequest, IEnumerable<SimilarTicketDetailModel>>
{
	private readonly ManageAlertService manageAlertService;
	private readonly IMapper mapper;

	public SimilarTicketDetailHandler(ManageAlertService manageAlertService, IMapper mapper)
	{
		this.manageAlertService = manageAlertService;
		this.mapper = mapper;
	}

	public Task<IEnumerable<SimilarTicketDetailModel>> Handle(SimilarTicketDetailRequest request, CancellationToken cancellationToken)
	{
		var criteria = mapper.Map<ViewDetailCriteria>(request);
		return manageAlertService.GetSimilarTicketDetail(criteria);
	}
}

public class PotentialWinningDetailHandler : IRequestHandler<PotentialWinningDetailRequest, IEnumerable<PotentialWinningDetailModel>>
{
	private readonly ManageAlertService manageAlertService;
	private readonly IMapper mapper;

	public PotentialWinningDetailHandler(ManageAlertService manageAlertService, IMapper mapper)
	{
		this.manageAlertService = manageAlertService;
		this.mapper = mapper;
	}

	public Task<IEnumerable<PotentialWinningDetailModel>> Handle(PotentialWinningDetailRequest request,
		CancellationToken cancellationToken)
	{
		var criteria = mapper.Map<ViewDetailCriteria>(request);
		return manageAlertService.GetPotentialWinningDetail(criteria);
	}
}