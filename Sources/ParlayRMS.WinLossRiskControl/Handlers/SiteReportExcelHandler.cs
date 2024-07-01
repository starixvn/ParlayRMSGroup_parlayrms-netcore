using AutoMapper;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Requests.Excels;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;

namespace ParlayRMS.WinLossRiskControl.Handlers;

public class SiteReportExcelHandler(WinLossService service, IMapper mapper, SiteExcelService excelService)
    : IRequestHandler<SiteWinLossExcelRequest, ExcelPackage>
{
    public async Task<ExcelPackage> Handle(SiteWinLossExcelRequest excelRequest, CancellationToken cancellationToken)
    {
        var excelSetting = new ExcelSetting
        {
            Title = "Site"
        };
        var request = mapper.Map<WinLossRequest>(excelRequest);
        var data = await service.SitesWinLoss(request);

        return excelService.CreateExcelPackage(data, excelSetting);
    }
}