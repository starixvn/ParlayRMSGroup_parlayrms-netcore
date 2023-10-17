using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using ParlayRMS.LeagueEventControl.Models.Excels;

namespace ParlayRMS.LeagueEventControl.Excels;

public interface ExcelBuilder
{
	ExcelPackage CreateExcelPackage(List<MainTicketExcelModel> list, ExcelSetting settings);
}

public class ExcelBuilderImp : ExcelBuilder
{
	private int startRowIndex;

	private ExcelWorksheet bodySheet;

	public ExcelPackage CreateExcelPackage(List<MainTicketExcelModel> list, ExcelSetting settings)
	{
		var excelPackage = LoadTemplate(settings.FileName);
		Build(list, settings);
		return excelPackage;
	}

	private ExcelPackage LoadTemplate(string fileName)
	{
		var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate", "LeagueEventControl", fileName);

		using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		var excelPackage = new ExcelPackage(stream);
		bodySheet = excelPackage.Workbook.Worksheets["League_EventMonitor"];
		return excelPackage;
	}

	private void Build(IEnumerable<MainTicketExcelModel> list, ExcelSetting settings)
	{
		SetStartRowIndex(settings.StartRowIndex);
		BuildTitle(settings.Title);
		ApplyBody(list);
	}

	private void ApplyBody(IEnumerable<MainTicketExcelModel> list)
	{
		bodySheet.ApplyVertical(list, startRowIndex);
	}

	private void BuildTitle(string title)
	{
		bodySheet.Cells["A1:M1"].Value = title;
	}

	private void SetStartRowIndex(int rowIndex)
	{
		startRowIndex = rowIndex;
	}
}