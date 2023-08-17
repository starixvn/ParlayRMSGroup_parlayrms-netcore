using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epplus.Mapper.Extensions;
using ParlayRMS.ManageAlert.Models;

namespace ParlayRMS.ManageAlert.Excels;

public interface ExcelBuilder
{
	ExcelPackage CreateExcelPackage(IEnumerable<ManageAlertExcelModel> list, ExcelSetting settings);
}

public class ExcelBuilderImp : ExcelBuilder
{
	private int startRowIndex;

	private ExcelWorksheet bodySheet;

	public ExcelPackage CreateExcelPackage(IEnumerable<ManageAlertExcelModel> list, ExcelSetting settings)
	{
		var excelPackage = LoadTemplate(settings.FileName!);
		Build(list, settings);
		return excelPackage;
	}

	private ExcelPackage LoadTemplate(string fileName)
	{
		var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate", "ManageAlert", fileName);

		using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		var excelPackage = new ExcelPackage(stream);
		bodySheet = excelPackage.Workbook.Worksheets["Manage_Alert"];
		return excelPackage;
	}

	private void Build(IEnumerable<ManageAlertExcelModel> list, ExcelSetting settings)
	{
		SetStartRowIndex(settings.StartRowIndex);
		ApplyBody(list);
	}

	private void ApplyBody(IEnumerable<ManageAlertExcelModel> list)
	{
		bodySheet.ApplyVertical(list, startRowIndex);
	}

	private void SetStartRowIndex(int rowIndex)
	{
		startRowIndex = rowIndex;
	}
}
