using System.Collections.Generic;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Requests
{
    public class AgentWinLossExcelRequest : IRequest<ExcelPackage>
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ParlayType { get; set; }

        public string SportType	 { get; set; }

        public int CustId { get; set; }

        public int RoleId { get; set; }

        public int PageSize { get;} = 0;

        public int PageIndex { get;} = 0;
    }
}