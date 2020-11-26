using System;
using System.IO;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Services
{
    public interface WinLossExcelService
    {
        ExcelPackage Build(WinLossExcelModel model);
    }

    public class WinLossExcelServiceImp : WinLossExcelService
    {
        private ExcelWorksheet actual;

        private ExcelWorksheet totalSheet;

        private const int StartRowIndex = 4;

        private ExcelPackage ExcelPackage { get; set; }

        public ExcelPackage Build(WinLossExcelModel model)
        {
            LoadTemplate();
            //actual.ApplyVertical(model.Rows, StartRowIndex);
            // ClearTemplate();
            return ExcelPackage;
        }

        private void LoadTemplate()
        {
            var templateAbsolutelyPath = $"{AppDomain.CurrentDomain.BaseDirectory}ExcelTemplate\\WinLossRiskControl\\member-win-loss.xlsx";
            using (var stream = new FileStream(templateAbsolutelyPath, FileMode.Open, FileAccess.Read))
            {
                ExcelPackage = new ExcelPackage(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                actual = ExcelPackage.Workbook.Worksheets.Copy("ItemTemplate", "Result");
                totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
            }
        }

        private void ClearTemplate()
        {
            ExcelPackage.Workbook.Worksheets.Delete("ItemTemplate");
            ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}