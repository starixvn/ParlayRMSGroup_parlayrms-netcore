using MediatR;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class SimilarTicketDetailHandler : IRequestHandler<SimilarTicketDetailRequest, IEnumerable<SimilarTicketDetailModel>>
{
	private readonly ManageAlertService manageAlertService;

	public SimilarTicketDetailHandler(ManageAlertService manageAlertService)
	{
		this.manageAlertService = manageAlertService;
	}

	public Task<IEnumerable<SimilarTicketDetailModel>> Handle(SimilarTicketDetailRequest request, CancellationToken cancellationToken) =>
		manageAlertService.GetSimilarTicketDetail(request);
}

public class PotentialWinningDetailHandler : IRequestHandler<PotentialWinningDetailRequest, IEnumerable<PotentialWinningDetailModel>>
{
	private readonly ManageAlertService manageAlertService;

	public PotentialWinningDetailHandler(ManageAlertService manageAlertService)
	{
		this.manageAlertService = manageAlertService;
	}

	public Task<IEnumerable<PotentialWinningDetailModel>> Handle(PotentialWinningDetailRequest request, CancellationToken cancellationToken) =>
		manageAlertService.GetPotentialWinningDetail(request);
}