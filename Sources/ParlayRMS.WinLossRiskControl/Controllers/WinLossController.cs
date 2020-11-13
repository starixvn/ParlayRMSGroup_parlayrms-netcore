using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.WinLossRiskControl.Requests;

namespace ParlayRMS.WinLossRiskControl
{
    [ApiController]
    [Route("api/win-loss-risk-control")]
    public class WinLossController : ControllerBase
    {
        private readonly IMediator mediator;
        public WinLossController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("get-site")]
        public async Task<ActionResult> GetSite(WinLossRequest request) {
            // var request = new SiteWinLossRequest {
            //     FromDate ="2020-09-01",// Convert.ToDateTime("2020-09-01"),
            //     ToDate = "2020-11-01",//Convert.ToDateTime("2020-11-01"),
            //     Company = "",
            //     ParlayType = "1,2,3,4",
            //     SportType = "1, -1, -2, -3"
            // };
            return Ok(await mediator.Send(request).ConfigureAwait(false));
        } 

        [Route("company-win-loss")]
        public async Task<ActionResult> GetCompany(CompanyWinLossRequest request) {
            return Ok(await mediator.Send(request).ConfigureAwait(false));
        } 

        [Route("agent-win-loss")]
        public async Task<ActionResult> GetAgent(AgentWinLossRequest request) {
            return Ok(await mediator.Send(request).ConfigureAwait(false));
        } 
    }
}
