using AutoMapper;
using MediatR;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class ManageAlertHandler(ManageAlertService alertService, IMapper mapper)
    : IRequestHandler<ManageAlertRequest, ManageAlertViewModel>
{
    public Task<ManageAlertViewModel> Handle(ManageAlertRequest request, CancellationToken cancellationToken)
	{
		var criteria = mapper.Map<ManageAlertCriteria>(request);
		return alertService.GetAlerts(criteria);
	}
}