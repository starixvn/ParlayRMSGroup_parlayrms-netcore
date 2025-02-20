using AutoMapper;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models.Excels;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Requests.Excels;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;

namespace ParlayRMS.WinLossRiskControl.Handlers;

public class ReportExcelHandler(WinLossService service, IMapper mapper, WinLossExcelBuilder builder)
    : IRequestHandler<AgentWinLossExcelRequest, ExcelPackage>,
        IRequestHandler<MemberWinLossExcelRequest, ExcelPackage>,
        IRequestHandler<SuperWinLossExcelRequest, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(SuperWinLossExcelRequest excelRequest, CancellationToken cancellationToken)
    {
        var excelSetting = new ExcelSetting
        {
            Title = excelRequest.BreadCrumb,
            Company = excelRequest.Company
        };
        var request = mapper.Map<SuperWinLossRequest>(excelRequest);
        var data = await service.SuperWinLoss(request);
        var total = mapper.Map<WinLossExcelModel>(data);
        var list = mapper.Map<List<WinLossExcelModel>>(data.Rows);

        return builder.CreateExcelPackage(list, total, excelSetting);
    }

    public async Task<ExcelPackage> Handle(AgentWinLossExcelRequest excelRequest, CancellationToken cancellationToken)
    {
        var excelSetting = new ExcelSetting
        {
            Title = excelRequest.BreadCrumb,
            RoleId = excelRequest.RoleId
        };
        var request = mapper.Map<AgentWinLossRequest>(excelRequest);

        var data = request.CustId < 1
            ? await service.AgentWinLossBySite(request)
            : await service.AgentWinLossByMaster(request);

        var total = mapper.Map<WinLossExcelModel>(data);
        var list = mapper.Map<List<WinLossExcelModel>>(data.Rows);
        var excelPackage = builder.CreateExcelPackage(list, total, excelSetting);
        if (excelRequest.isDeposit)
        {
            excelPackage.Workbook.Worksheets["Result"].Cells["A2"].Value = "Agent";
        }

        return excelPackage;
    }

    public async Task<ExcelPackage> Handle(MemberWinLossExcelRequest excelRequest, CancellationToken cancellationToken)
    {
        var excelSetting = new ExcelSetting
        {
            Title = excelRequest.BreadCrumb,
            RoleId = excelRequest.RoleId,
            IsMerged = true
        };

        var request = mapper.Map<AgentWinLossRequest>(excelRequest);
        var data = await service.AgentWinLossByMaster(request);

        var total = mapper.Map<MemberWinLossExcelModel>(data);
        var list = mapper.Map<List<MemberWinLossExcelModel>>(data.Rows);
        return builder.CreateExcelPackage(list, total, excelSetting);
    }
}