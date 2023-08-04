using System.Collections.Generic;
using System.IO;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models;
using AutoMapper;
using ParlayRMS.WinLossRiskControl.Models.Excels;

namespace ParlayRMS.WinLossRiskControl.Shared;

public interface SiteExcelService
{
	ExcelPackage CreateExcelPackage(WinLossModel model, ExcelSetting settings);
}

public class SiteExcelServiceImp : SiteExcelService
{
	private ExcelWorksheet bodySheet;

	private ExcelWorksheet totalSheet;

	private ExcelWorksheet groupSheet;

	private readonly IMapper mapper;

	private int startRowIndex;

	public SiteExcelServiceImp(IMapper mapper)
	{
		this.mapper = mapper;
	}

	public ExcelPackage CreateExcelPackage(WinLossModel model, ExcelSetting settings)
	{
		var excelPackage = LoadTemplate(settings.FileName);
		BuildBody(model.Rows, settings);
		BuildTotal(model, settings);
		ClearTemplate(excelPackage);
		return excelPackage;
	}

	private void BuildTotal(WinLossModel model, ExcelSetting settings)
	{
		SetSheetName(settings.SheetName);
		BuilTitle(settings.Title);
		ApplyTotal(model, totalSheet, startRowIndex);
	}

	private void BuildBody(List<WinLossModel> rows, ExcelSetting settings)
	{
		var credit = new WinLossModel();
		var deposit = new WinLossModel();
		var depositRowIndex = 0;
		startRowIndex = settings.StartRowIndex;
		foreach (var item in rows)
		{
			if (item.IsDeposit == true)
			{
				deposit = item;
				depositRowIndex = startRowIndex;
			}
			else
			{
				credit = item;
			}

			startRowIndex++;
			ApplyBody(item.Rows, startRowIndex);
			startRowIndex += item.Rows.Count;
		}

		ApplyTotal(credit, groupSheet, settings.StartRowIndex);
		ApplyTotal(deposit, groupSheet, depositRowIndex);
	}

	private void ApplyBody(List<WinLossModel> rows, int rowIndex)
	{
		var list = mapper.Map<List<WinLossExcelModel>>(rows);
		bodySheet.ApplyVertical(list, rowIndex);
	}

	private void ApplyTotal(WinLossModel model, ExcelWorksheet sheet, int rowIndex)
	{
		if (model.NoMember <= 0) return;
		var total = mapper.Map<WinLossExcelModel>(model);
		bodySheet.ApplyModel(sheet, total, rowIndex);
	}

	private ExcelPackage LoadTemplate(string fileName)
	{
		var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate", "WinLossRiskControl", fileName);

		using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		var excelPackage = new ExcelPackage(stream);
		bodySheet = excelPackage.Workbook.Worksheets.Copy("Item", "Result");
		totalSheet = excelPackage.Workbook.Worksheets["Total"];
		groupSheet = excelPackage.Workbook.Worksheets["Group"];
		return excelPackage;
	}

	private void SetSheetName(string sheetName)
	{
		bodySheet.Name = sheetName;
	}

	private void BuilTitle(string title)
	{
		bodySheet.Cells["A1:I1"].Value = title;
	}

	private static void ClearTemplate(ExcelPackage excelPackage)
	{
		excelPackage.Workbook.Worksheets.Delete("Item");
		excelPackage.Workbook.Worksheets.Delete("Total");
		excelPackage.Workbook.Worksheets.Delete("Group");
	}
}