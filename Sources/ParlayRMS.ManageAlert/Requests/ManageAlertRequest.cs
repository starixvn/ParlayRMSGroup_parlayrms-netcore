using MediatR;
using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.Requests
{
    public class ManageAlertRequest : IRequest<IEnumerable<ManageAlertModel>>
    {
        public required string FromDate { get; set; }

        public required string ToDate { get; set; }

        public required string AlertType { get; set; }

        public string? Member { get; set; }

        public string? CheckedBy { get; set; }

        public bool IsNoChecked { get; set; }

        public required string ActionType { get; set; }

        public required string TicketStatus { get; set; }
    }
}
