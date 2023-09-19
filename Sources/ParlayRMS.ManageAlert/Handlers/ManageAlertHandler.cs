using AutoMapper;
using MediatR;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class ManageAlertHandler : IRequestHandler<ManageAlertRequest, ManageAlertViewModel>
{
	private readonly ManageAlertService manageAlertService;

	private readonly IMapper mapper;

	public ManageAlertHandler(ManageAlertService manageAlertService, IMapper mapper)
	{
		this.manageAlertService = manageAlertService;
		this.mapper = mapper;
	}

	public Task<ManageAlertViewModel> Handle(ManageAlertRequest request, CancellationToken cancellationToken)
	{
		var criteria = mapper.Map<ManageAlertCriteria>(request);
		return manageAlertService.GetAlerts(criteria);
	}
}