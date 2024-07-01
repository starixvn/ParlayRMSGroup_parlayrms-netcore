using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ParlayRMS.WinLossRiskControl.Epplus;
using ParlayRMS.WinLossRiskControl.Requests.Excels;

namespace ParlayRMS.WinLossRiskControl.Controllers;

[ApiController]
[Route("api/win-loss-risk-control")]
[EnableCors("AllowOrigin")]
public class WinLossExcelController(IMediator mediator)
{
    [Route("site-win-loss-excel")]
    public async Task<IActionResult> ExcelSite(SiteWinLossExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("company-win-loss-excel")]
    public async Task<IActionResult> ExcelCompany(SuperWinLossExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("agent-win-loss-excel")]
    public async Task<IActionResult> ExcelAgent(AgentWinLossExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }

    [Route("member-win-loss-excel")]
    public async Task<IActionResult> ExcelMember(MemberWinLossExcelRequest request)
    {
        var excelPackage = await mediator.Send(request);
        return new EpplusExcelResult(excelPackage);
    }
}