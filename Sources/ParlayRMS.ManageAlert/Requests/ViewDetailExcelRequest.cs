using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.ManageAlert.Requests;

public class SimilarTicketDetailExcelRequest : IRequest<ExcelPackage>
{
	public long AlertId { get; set; }

	public required string Selection { get; set; }

	public decimal? Odds { get; set; }

	public required string TicketStatus { get; set; }
}

public class PotentialWinningDetailExcelRequest : IRequest<ExcelPackage>
{
	public long AlertId { get; set; }

	public required string Selection { get; set; }

	public decimal? Odds { get; set; }

	public required string TicketStatus { get; set; }
}
