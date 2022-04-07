using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.WinLossRiskControl.Requests;
using System.Threading.Tasks;

namespace ParlayRMS.WinLossRiskControl.Controllers
{
    [ApiController]
    [Route("api/win-loss-risk-control/[controller]")]
    [EnableCors("AllowOrigin")]
    public class FilterController : ControllerBase
    {
        private readonly IMediator mediator;

        public FilterController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("sports")]
        public async Task<ActionResult> Sports() => Ok(await mediator.Send(new SportRequest()));

        [Route("parlay-types")]
        public async Task<ActionResult> ParlayTypes() => Ok(await mediator.Send(new ParlayTypeRequest()));

        [Route("company")]
        public async Task<ActionResult> Company() => Ok(await mediator.Send(new CompanyRequest()));

        [Route("comboes")]
        public async Task<ActionResult> Comboes() => Ok(await mediator.Send(new ComboRequest()));
    }
}