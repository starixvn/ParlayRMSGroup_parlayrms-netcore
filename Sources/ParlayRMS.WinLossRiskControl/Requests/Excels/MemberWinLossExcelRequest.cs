using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.WinLossRiskControl.Requests.Excels;

public class MemberWinLossExcelRequest : IRequest<ExcelPackage>
{
    public string BreadCrumb { get; set; }

    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public string ParlayType { get; set; }

    public string Selection { get; set; }

    public string SportType { get; set; }

    public int CustId { get; set; }

    public int RoleId { get; set; }

    public string Site { get; set; }

    public int PageSize => 0;

    public int PageIndex => 0;

    public byte LiveStatus { get; set; }
}