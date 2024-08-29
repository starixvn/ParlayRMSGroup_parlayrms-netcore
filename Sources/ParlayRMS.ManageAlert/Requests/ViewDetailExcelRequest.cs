using MediatR;
using OfficeOpenXml;

namespace ParlayRMS.ManageAlert.Requests;

public class SimilarTicketDetailExcelRequest : IRequest<ExcelPackage>
{
    public long AlertId { get; set; }

    public string Selection { get; set; }

    public decimal? Odds { get; set; }

    public string TicketStatus { get; set; }
}

public class PotentialWinningDetailExcelRequest : IRequest<ExcelPackage>
{
    public long AlertId { get; set; }

    public string Selection { get; set; }

    public decimal? Odds { get; set; }

    public string TicketStatus { get; set; }
}