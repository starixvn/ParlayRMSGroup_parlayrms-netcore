using Epplus.Mapper.Extensions;
using OfficeOpenXml;

namespace ParlayRMS.ManageAlert.Excels;

public interface ExcelBuilder
{
	ExcelPackage CreateExcelPackage<T>(IEnumerable<T> list, ExcelSetting settings) where T : class, new();
}

public class ExcelBuilderImp : ExcelBuilder
{
	private int startRowIndex;

	private ExcelWorksheet bodySheet;

	private static readonly string[] sheetNames = { "Manage_Alert", "Similar_Bet", "Potential_Winning" };

	public ExcelPackage CreateExcelPackage<T>(IEnumerable<T> list, ExcelSetting settings) where T : class, new()
	{
		var excelPackage = LoadTemplate(settings.FileName!, settings.SheetName);
		Build(list, settings);
		ClearTemplate(excelPackage, settings.SheetName);
		return excelPackage;
	}

	private ExcelPackage LoadTemplate(string fileName, string sheetName)
	{
		var filePath = Path.Combine("ExcelTemplates", "ManageAlert", fileName);

		using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		var excelPackage = new ExcelPackage(stream);
		bodySheet = excelPackage.Workbook.Worksheets[sheetName];
		return excelPackage;
	}

	private void Build<T>(IEnumerable<T> list, ExcelSetting settings) where T : class, new()
	{
		SetStartRowIndex(settings.StartRowIndex);
		ApplyBody(list);
	}

	private void ApplyBody<T>(IEnumerable<T> list) where T : class, new()
	{
		bodySheet.ApplyVertical(list, startRowIndex);
	}

	private void SetStartRowIndex(int rowIndex)
	{
		startRowIndex = rowIndex;
	}

	private static void ClearTemplate(ExcelPackage excelPackage, string sheetName)
	{
		var list = sheetNames.Where(s => s != sheetName);
		foreach (var item in list)
		{
			excelPackage.Workbook.Worksheets.Delete(item);
		}
	}
}
