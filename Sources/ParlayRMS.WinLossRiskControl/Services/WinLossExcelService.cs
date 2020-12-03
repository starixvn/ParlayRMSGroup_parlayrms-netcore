using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Models.Excels;

namespace ParlayRMS.WinLossRiskControl.Services
{
    public interface WinLossExcelService
    {
        //ExcelPackage CreateExcelPackageMaster(string title, WinLossModel model);

        ExcelPackage CreateExcelPackageSuper(string title, WinLossModel model);

        ExcelPackage CreateExcelPackageMember(string title, WinLossModel model);
    }

    public class WinLossExcelServiceImp : WinLossExcelService
    {
        private const int StartRowIndex = 3;

        private readonly IMapper mapper;

        private ExcelWorksheet sheet;

        private ExcelWorksheet totalSheet;

        public WinLossExcelServiceImp(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ExcelPackage CreateExcelPackageSuper(string title, WinLossModel model)
        {
            var excelPackage = LoadTemplate("super-win-loss.xlsx");
            BuilTitle(title);
            ApplyBody<WinLossModel, WinLossExcelModel>(model.Rows);
            ApplyTotal<WinLossModel, WinLossExcelModel>(model, StartRowIndex + model.Rows.Count);
            ClearTemplate(excelPackage);
            return excelPackage;
        }

        public ExcelPackage CreateExcelPackageMember(string title, WinLossModel model)
        {
            var excelPackage = LoadTemplate("member-win-loss.xlsx");
            BuilTitle(title);
            ApplyBody<WinLossModel, MemberWinLossExcelModel>(model.Rows);
            ApplyTotal<WinLossModel, MemberWinLossExcelModel>(model, StartRowIndex + model.Rows.Count);
            ClearTemplate(excelPackage);
            return excelPackage;
        }

        private void ApplyBody<T1, T2>(List<T1> result) where T1 : class, new()
        {
            var mapperResult = mapper.Map<List<T2>>(result); ;
            sheet.ApplyVertical(mapperResult, StartRowIndex);
        }

        private void ApplyTotal<T1, T2>(T1 total, int totalIndex) where T1 : class, new()
        {
            var mapperTotal = mapper.Map<T2>(total);
            sheet.ApplyModel(totalSheet, mapperTotal, totalIndex);
        }

        private ExcelPackage LoadTemplate(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate", "WinLossRiskControl", fileName);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var excelPackage = new ExcelPackage(stream);
                sheet = excelPackage.Workbook.Worksheets.Copy("ItemTemplate", "Result");
                totalSheet = excelPackage.Workbook.Worksheets["TotalTemplate"];
                return excelPackage;
            }
        }

        private void BuilTitle(string title)
        {
            sheet.Cells["A1:I1"].Value = title;
        }

        private static void ClearTemplate(ExcelPackage excelPackage)
        {
            excelPackage.Workbook.Worksheets.Delete("ItemTemplate");
            excelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}