using MediatR;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class TakeActionHandler(ManageAlertService alertService) : IRequestHandler<TakeActionRequest, int>
{
    public Task<int> Handle(TakeActionRequest request, CancellationToken cancellationToken) =>
		alertService.TakeAction(request);
}