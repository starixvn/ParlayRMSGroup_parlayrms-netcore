using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.ManageAlert.Requests;

public class ManageAlertExcelRequest : IRequest<ExcelPackage>
{
	public required string FromDate { get; set; }

	public required string ToDate { get; set; }

	public required string AlertType { get; set; }

	public string? Member { get; set; }

	public string? CheckBy { get; set; }

	public bool IsNoChecked { get; set; }

	public required string ActionType { get; set; }

	public required string TicketStatus { get; set; }

	public int PageIndex { get; set; }

	public int PageSize { get; set; }
}
