using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Requests.Excels;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;

namespace ParlayRMS.WinLossRiskControl.Handlers;

public class SiteReportExcelHandler : IRequestHandler<SiteWinLossExcelRequest, ExcelPackage>
{
	private readonly IMapper mapper;
	private readonly WinLossService service;
	private readonly SiteExcelService siteExcelService;

	public SiteReportExcelHandler(WinLossService service, IMapper mapper, SiteExcelService siteExcelService)
	{
		this.service = service;
		this.mapper = mapper;
		this.siteExcelService = siteExcelService;
	}

	public async Task<ExcelPackage> Handle(SiteWinLossExcelRequest excelRequest, CancellationToken cancellationToken)
	{
		var excelSetting = new ExcelSetting
		{
			Title = "Site"
		};
		var request = mapper.Map<WinLossRequest>(excelRequest);
		var data = await service.SitesWinLoss(request);

		return siteExcelService.CreateExcelPackage(data, excelSetting);
	}
}