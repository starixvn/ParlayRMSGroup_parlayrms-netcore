using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.Handlers
{
    public class ReportExcelHandler : IRequestHandler<AgentWinLossExcelRequest, ExcelPackage>
                                //IRequestHandler<CompanyWinLossRequest, WinLossModel>,
                                //IRequestHandler<AgentWinLossRequest, AgentWinLossModel>
    {
        private readonly IMapper mapper;
        private readonly WinLossService service;
        private readonly WinLossExcelService excelService;
        public ReportExcelHandler(WinLossService service, WinLossExcelService excelService,  IMapper mapper)
        {
            this.service = service;
            this.excelService = excelService;
            this.mapper = mapper;
        }

        // public async Task<WinLossModel> Handle(WinLossRequest request, CancellationToken cancellationToken)
        // {
        //     return await service.GetSites(request);
        // }     

        // public async Task<WinLossModel> Handle(CompanyWinLossRequest request, CancellationToken cancellationToken)
        // {
           
        //     return await service.GetCompanyWinLoss(request);
        // }   
        
        public async Task<ExcelPackage> Handle(AgentWinLossExcelRequest excelRequest, CancellationToken cancellationToken)
        {
            var request = mapper.Map<AgentWinLossRequest>(excelRequest);
            var data = await service.GetAgentWinLoss(request);
            var excelData = mapper.Map<WinLossExcelModel>(data);
            var excelPackage = excelService.Build(excelData);
            return excelPackage;

        }   
    }
}