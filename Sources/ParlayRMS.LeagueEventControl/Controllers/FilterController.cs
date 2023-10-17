using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl.Controllers;

[ApiController]
[Route("api/league-event-control/[controller]")]
[EnableCors("AllowOrigin")]
public class FilterController : ControllerBase
{
	private readonly IMediator mediator;

	public FilterController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[Route("league")]
	public async Task<ActionResult> League(LeagueRequest request) => Ok(await mediator.Send(request));

	[Route("bettype")]
	public async Task<ActionResult> Bettype(BettypeRequest request) => Ok(await mediator.Send(request));
}