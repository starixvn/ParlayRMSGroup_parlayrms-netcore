using MediatR;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers
{
    public class TakeActionHandler : IRequestHandler<TakeActionRequest, int>
    {
        private readonly ManageAlertService manageAlertService;

        public TakeActionHandler(ManageAlertService manageAlertService)
        {
            this.manageAlertService = manageAlertService;
        }

        public Task<int> Handle(TakeActionRequest request, CancellationToken cancellationToken) =>
            manageAlertService.TakeAction(request);
    }
}
