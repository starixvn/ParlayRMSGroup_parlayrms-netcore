using System.Collections.Generic;
using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests
{
    public class AgentWinLossRequest : IRequest<AgentWinLossModel>
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ParlayType { get; set; }

        public string SportType	 { get; set; }

        public int CustId { get; set; }

        public int RoleId { get; set; }

        public int PageSize { get; set; }

        public int PageIndex { get; set; }
    }
}