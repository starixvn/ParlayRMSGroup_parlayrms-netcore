using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace ParlayRMS.ManageAlert.Epplus;

public class EpplusExcelResult(ExcelPackage excelPackage) : IActionResult
{
	public ExcelPackage ExcelPackage = excelPackage;

    public async Task ExecuteResultAsync(ActionContext context)
	{
		var byteArray = await ExcelPackage.GetAsByteArrayAsync();
		var response = context.HttpContext.Response;
		response.Headers.Add("Content-Type", "application/vnd.ms-excel");
		response.Headers.Add("Content-Disposition", new[] { "attachment; filename=Excel.xlsx" });
		await response.Body.WriteAsync(byteArray.AsMemory(0, byteArray.Length));
	}
}
