using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.WinLossRiskControl.Requests.Excels
{
    public class SiteWinLossExcelRequest : IRequest<ExcelPackage>
    {
        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string ParlayType { get; set; }

        public string SportType { get; set; }

        public string Company { get; set; }
    }
}