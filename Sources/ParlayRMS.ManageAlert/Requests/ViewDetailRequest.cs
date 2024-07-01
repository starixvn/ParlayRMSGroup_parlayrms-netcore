using MediatR;
using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.Requests;

public class SimilarTicketDetailRequest : IRequest<IEnumerable<SimilarTicketDetailModel>>
{
    public long AlertId { get; set; }

    public string Selection { get; set; }

    public decimal? Odds { get; set; }

    public string TicketStatus { get; set; }
}

public class PotentialWinningDetailRequest : IRequest<IEnumerable<PotentialWinningDetailModel>>
{
    public long AlertId { get; set; }

    public string Selection { get; set; }

    public decimal? Odds { get; set; }

    public string TicketStatus { get; set; }
}