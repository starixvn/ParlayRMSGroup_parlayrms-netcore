using AutoMapper;
using MediatR;
using OfficeOpenXml;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Excels;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.Handlers;

public class ExcelHandler : IRequestHandler<ManageAlertExcelRequest, ExcelPackage>,
	IRequestHandler<SimilarTicketDetailExcelRequest, ExcelPackage>,
	IRequestHandler<PotentialWinningDetailExcelRequest, ExcelPackage>
{
	private readonly ExcelBuilder excelBuilder;

	private readonly IMapper mapper;

	private readonly ManageAlertService manageAlertService;

	public ExcelHandler(ExcelBuilder excelBuilder, IMapper mapper, ManageAlertService manageAlertService)
	{
		this.excelBuilder = excelBuilder;
		this.mapper = mapper;
		this.manageAlertService = manageAlertService;
	}

	public async Task<ExcelPackage> Handle(ManageAlertExcelRequest request, CancellationToken cancellationToken)
	{
		var excelSetting = new ExcelSetting
		{
			FileName = "manage-alert.xlsx",
			SheetName = "Manage_Alert"
		};
		var criteria = mapper.Map<ManageAlertCriteria>(request);
		var data = await manageAlertService.GetAlerts(criteria);
		var list = mapper.Map<List<ManageAlertExcelModel>>(data.Items);
		UpdateViewModel(list);
		return excelBuilder.CreateExcelPackage(list, excelSetting);
	}

	public async Task<ExcelPackage> Handle(SimilarTicketDetailExcelRequest request, CancellationToken cancellationToken)
	{
		var excelSetting = new ExcelSetting
		{
			FileName = "manage-alert.xlsx",
			SheetName = "Similar_Bet"
		};
		var criteria = mapper.Map<ViewDetailCriteria>(request);
		var data = await manageAlertService.GetSimilarTicketDetail(criteria);
		var list = mapper.Map<List<SimilarTicketDetailExcelModel>>(data);
		var index = 1;
		foreach (var item in list) item.No = index++;
		return excelBuilder.CreateExcelPackage(list, excelSetting);
	}

	public async Task<ExcelPackage> Handle(PotentialWinningDetailExcelRequest request, CancellationToken cancellationToken)
	{
		var excelSetting = new ExcelSetting
		{
			FileName = "manage-alert.xlsx",
			SheetName = "Potential_Winning"
		};
		var criteria = mapper.Map<ViewDetailCriteria>(request);
		var data = await manageAlertService.GetPotentialWinningDetail(criteria);
		var list = mapper.Map<List<PotentialWinningDetailExcelModel>>(data);
		var index = 1;
		foreach (var item in list) item.No = index++;
		return excelBuilder.CreateExcelPackage(list, excelSetting);
	}

	private static void UpdateViewModel(IEnumerable<ManageAlertExcelModel> list)
	{
		var index = 1;
		foreach (var item in list)
		{
			item.No = index;
			item.BetId = string.IsNullOrEmpty(item.BetId) ? "-" : item.BetId;
			item.SimilarTickets = string.IsNullOrEmpty(item.SimilarTickets) ? "-" : item.SimilarTickets;
			item.TotalBets = string.IsNullOrEmpty(item.TotalBets) ? "-" : item.TotalBets;
			item.CheckedBy = string.IsNullOrEmpty(item.CheckedBy) ? "None" : item.CheckedBy;
			index++;
		}
	}
}
