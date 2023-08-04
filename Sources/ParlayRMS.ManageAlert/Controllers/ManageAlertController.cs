using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.ManageAlert.Requests;

namespace ParlayRMS.ManageAlert.Controllers;

[ApiController]
[Route("api/manage-alert")]
[EnableCors("AllowOrigin")]
public class ManageAlertController : ControllerBase
{
	private readonly IMediator mediator;

	public ManageAlertController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[Route("alerts")]
	public async Task<IActionResult> Alerts(ManageAlertRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("similar-detail")]
	public async Task<IActionResult> Detail(SimilarTicketDetailRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("potential-winning-detail")]
	public async Task<IActionResult> Detail(PotentialWinningDetailRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("log")]
	public async Task<IActionResult> Logs(ViewLogRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("take-action")]
	public async Task<IActionResult> TakeAction(TakeActionRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));
}