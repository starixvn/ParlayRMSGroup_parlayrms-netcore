using MediatR;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class ViewLogHandler(ManageAlertService alertService)
    : IRequestHandler<ViewLogRequest, IEnumerable<ViewLogModel>>
{
    public Task<IEnumerable<ViewLogModel>> Handle(ViewLogRequest request, CancellationToken cancellationToken) =>
		alertService.GetViewLogs(request);
}