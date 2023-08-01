using MediatR;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers
{
    public class ViewLogHandler : IRequestHandler<ViewLogRequest, IEnumerable<ViewLogModel>>
    {
        private readonly ManageAlertService manageAlertService;

        public ViewLogHandler(ManageAlertService manageAlertService)
        {
            this.manageAlertService = manageAlertService;
        }

        public Task<IEnumerable<ViewLogModel>> Handle(ViewLogRequest request, CancellationToken cancellationToken) =>
            this.manageAlertService.GetViewLogs(request);
    }
}
