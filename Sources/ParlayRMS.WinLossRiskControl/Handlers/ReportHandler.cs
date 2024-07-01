using MediatR;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.Handlers;

public class ReportHandler(WinLossService service) : IRequestHandler<WinLossRequest, WinLossModel>,
    IRequestHandler<SuperWinLossRequest, WinLossModel>,
    IRequestHandler<AgentWinLossRequest, WinLossModel>
{
    public Task<WinLossModel> Handle(WinLossRequest request, CancellationToken cancellationToken)
    {
        return service.SitesWinLoss(request);
    }

    public Task<WinLossModel> Handle(SuperWinLossRequest request, CancellationToken cancellationToken)
    {
        return service.SuperWinLoss(request);
    }

    public async Task<WinLossModel> Handle(AgentWinLossRequest request, CancellationToken cancellationToken)
    {
        return request.CustId < 1
            ? await service.AgentWinLossBySite(request)
            : await service.AgentWinLossByMaster(request);
    }
}