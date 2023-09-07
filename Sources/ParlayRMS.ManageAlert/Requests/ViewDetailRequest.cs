using MediatR;
using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.Requests;

public class SimilarTicketDetailRequest : IRequest<IEnumerable<SimilarTicketDetailModel>>
{
	public long AlertId { get; set; }

	public required string Selection { get; set; }
}

public class PotentialWinningDetailRequest : IRequest<IEnumerable<PotentialWinningDetailModel>>
{
	public long AlertId { get; set; }

	public required string Selection { get; set; }
}