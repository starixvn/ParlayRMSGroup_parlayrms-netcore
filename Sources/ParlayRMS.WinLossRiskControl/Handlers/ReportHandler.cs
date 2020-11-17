using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.Handlers
{
    public class ReportHandler : IRequestHandler<WinLossRequest, WinLossModel>,
                                IRequestHandler<CompanyWinLossRequest, WinLossModel>,
                                IRequestHandler<AgentWinLossRequest, AgentWinLossModel>
    {
        private readonly IMapper mapper;
        private readonly WinLossService service;
        public ReportHandler(WinLossService service,  IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        public async Task<WinLossModel> Handle(WinLossRequest request, CancellationToken cancellationToken)
        {
            return await service.GetSites(request);
        }     

        public async Task<WinLossModel> Handle(CompanyWinLossRequest request, CancellationToken cancellationToken)
        {
           
            return await service.GetCompanyWinLoss(request);
        }   
        
        public async Task<AgentWinLossModel> Handle(AgentWinLossRequest request, CancellationToken cancellationToken)
        {
            return await service.GetAgentWinLoss(request);
        }   
    }
}