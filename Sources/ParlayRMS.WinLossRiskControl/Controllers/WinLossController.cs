using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.WinLossRiskControl.Requests;
using System.Threading.Tasks;

namespace ParlayRMS.WinLossRiskControl;

[ApiController]
[Route("api/win-loss-risk-control")]
[EnableCors("AllowOrigin")]
public class WinLossController : ControllerBase
{
	private readonly IMediator mediator;

	public WinLossController(IMediator mediator)
	{
		this.mediator = mediator;
	}

	[Route("get-site")]
	public async Task<IActionResult> GetSite(WinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("company-win-loss")]
	public async Task<IActionResult> GetCompany(SuperWinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));

	[Route("agent-win-loss")]
	public async Task<IActionResult> GetAgent(AgentWinLossRequest request) => Ok(await mediator.Send(request).ConfigureAwait(false));
}