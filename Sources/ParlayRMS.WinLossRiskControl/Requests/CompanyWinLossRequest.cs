using System.Collections.Generic;
using MediatR;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests
{
    public class CompanyWinLossRequest : WinLossRequest
    {
        public bool IsDeposit { get; set; }
    }
}