using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.ManageAlert.Requests;

public class ManageAlertExcelRequest : IRequest<ExcelPackage>
{
    public string FromDate { get; set; }

    public string ToDate { get; set; }

    public string AlertType { get; set; }

    public string? Member { get; set; }

    public string? CheckBy { get; set; }

    public bool IsNoChecked { get; set; }

    public string ActionType { get; set; }

    public string TicketStatus { get; set; }

    public string SportType { get; set; } = string.Empty;

    public string Selection { get; set; }

    public decimal? Odds { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}