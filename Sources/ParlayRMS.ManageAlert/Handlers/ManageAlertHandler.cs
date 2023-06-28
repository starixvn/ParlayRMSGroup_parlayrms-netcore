using MediatR;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers
{
    public class ManageAlertHandler : IRequestHandler<ManageAlertRequest, IEnumerable<ManageAlertModel>>
    {
        private readonly ManageAlertService manageAlertService;

        public ManageAlertHandler(ManageAlertService manageAlertService)
        {
            this.manageAlertService = manageAlertService;
        }

        public Task<IEnumerable<ManageAlertModel>> Handle(ManageAlertRequest request,
            CancellationToken cancellationToken) => this.manageAlertService.GetAlerts(request);
    }
}
