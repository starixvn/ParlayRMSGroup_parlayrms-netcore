using MediatR;
using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.Requests;

public class ViewLogRequest : IRequest<IEnumerable<ViewLogModel>>
{
	public long AlertId { get; set; }
}