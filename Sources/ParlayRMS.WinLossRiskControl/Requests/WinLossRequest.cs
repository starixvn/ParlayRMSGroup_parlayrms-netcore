using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests
{
    public class WinLossRequest : IRequest<WinLossModel>
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Company { get; set; }

        public string ParlayType { get; set; }

        public string SportType	 { get; set; }
    }
}