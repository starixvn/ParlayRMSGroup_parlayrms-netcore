using MediatR;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.Handlers;

public class ReportHandler : IRequestHandler<WinLossRequest, WinLossModel>,
	IRequestHandler<SuperWinLossRequest, WinLossModel>,
	IRequestHandler<AgentWinLossRequest, WinLossModel>
{
	private readonly WinLossService service;

	public ReportHandler(WinLossService service)
	{
		this.service = service;
	}

	public async Task<WinLossModel> Handle(WinLossRequest request, CancellationToken cancellationToken)
	{
		return await service.SitesWinLoss(request);
	}

	public async Task<WinLossModel> Handle(SuperWinLossRequest request, CancellationToken cancellationToken)
	{
		return await service.SuperWinLoss(request);
	}

	public async Task<WinLossModel> Handle(AgentWinLossRequest request, CancellationToken cancellationToken)
	{
		return request.CustId < 1
			? await service.AgentWinLossBySite(request)
			: await service.AgentWinLossByMaster(request);
	}
}