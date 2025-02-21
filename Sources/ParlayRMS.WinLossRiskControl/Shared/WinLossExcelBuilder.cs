using Epplus.Mapper.Extensions;
using OfficeOpenXml;

namespace ParlayRMS.WinLossRiskControl.Shared;

public interface WinLossExcelBuilder
{
    ExcelPackage CreateExcelPackage<T>(List<T> list, T total, ExcelSetting settings) where T : class, new();
}

public class WinLossExcelBuilderImp : WinLossExcelBuilder
{
    private int startRowIndex;

    private int endRowIndex;

    private ExcelWorksheet bodySheet;

    private ExcelWorksheet totalSheet;

    public ExcelPackage CreateExcelPackage<T>(List<T> list, T total, ExcelSetting settings) where T : class, new()
    {
        var excelPackage = LoadTemplate(settings.FileName);
        Build(list, total, settings);
        ClearTemplate(excelPackage);

        return excelPackage;
    }

    private ExcelPackage LoadTemplate(string fileName)
    {
        var filePath = Path.Combine("ExcelTemplates", "WinLossRiskControl", fileName);

        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var excelPackage = new ExcelPackage(stream);
        bodySheet = excelPackage.Workbook.Worksheets.Copy("Item", "Result");
        totalSheet = excelPackage.Workbook.Worksheets["Total"];
        return excelPackage;
    }

    private void Build<T>(IReadOnlyCollection<T> list, T total, ExcelSetting settings) where T : class, new()
    {
        SetStartRowIndex(settings.StartRowIndex);
        SetEndRowIndex(settings.StartRowIndex, list.Count);
        SetSheetName(settings.SheetName);
        BuildTitle(settings.Title);
        ApplyBody(list);
        ApplyTotal(total);
        MergeTotalCell(settings.IsMerged);
    }

    private void SetStartRowIndex(int rowIndex)
    {
        startRowIndex = rowIndex;
    }

    private void SetEndRowIndex(int rowIndex, int numberBodyItems)
    {
        endRowIndex = rowIndex + numberBodyItems;
    }

    private void SetSheetName(string sheetName)
    {
        bodySheet.Name = sheetName;
    }

    private void BuildTitle(string title)
    {
        bodySheet.Cells["A1:K1"].Value = title;
    }

    private void ApplyBody<T>(IEnumerable<T> list) where T : class, new()
    {
        bodySheet.ApplyVertical(list, startRowIndex);
    }

    private void ApplyTotal<T>(T total) where T : class, new()
    {
        bodySheet.ApplyModel(totalSheet, total, endRowIndex);
    }

    public void MergeTotalCell(bool isMerged)
    {
        if (isMerged)
        {
            bodySheet.Cells[$"A{endRowIndex}:B{endRowIndex}"].Merge = true;
        }
        else
        {
            bodySheet.Cells[$"C{endRowIndex}"].Value = string.Empty;
        }
    }

    private static void ClearTemplate(ExcelPackage excelPackage)
    {
        excelPackage.Workbook.Worksheets.Delete("Item");
        excelPackage.Workbook.Worksheets.Delete("Total");
    }
}