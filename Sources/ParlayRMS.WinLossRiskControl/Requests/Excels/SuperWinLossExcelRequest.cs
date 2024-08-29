using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.WinLossRiskControl.Requests.Excels;

public class SuperWinLossExcelRequest : IRequest<ExcelPackage>
{
    public string BreadCrumb { get; set; }

    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public string ParlayType { get; set; }

    public string Selection { get; set; }

    public string SportType { get; set; }

    public string Company { get; set; }

    public byte LiveStatus { get; set; }
}