using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Epplus.Mapper.Extensions;
using OfficeOpenXml;
using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.Services
{
    public interface WinLossExcelService
    {
        ExcelPackage Build(string title, AgentWinLossModel model);
    }

    public class WinLossExcelServiceImp : WinLossExcelService
    {
        private readonly IMapper mapper;

        private ExcelWorksheet sheet;

        private ExcelWorksheet totalSheet;

        private const int StartRowIndex = 4;

        private ExcelPackage ExcelPackage { get; set; }

        public WinLossExcelServiceImp(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ExcelPackage Build(string title, AgentWinLossModel model)
        {
            LoadTemplate();
            BuilTitle(title);
            BuildRow(model);
            BuildTotal(model);
            ClearTemplate();
            return ExcelPackage;
        }

        private void BuildRow(AgentWinLossModel model)
        {
            var excelData = mapper.Map<WinLossExcelModel>(model);
            sheet.ApplyVertical(excelData.Rows, StartRowIndex);
        }

        private void BuildTotal(AgentWinLossModel model)
        {
            var excelData = mapper.Map<WinLossExcelModel>(model);
            sheet.ApplyModel(totalSheet, excelData, StartRowIndex + model.Rows.Count);
        }

        private void LoadTemplate()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate", "WinLossRiskControl", "member-win-loss.xlsx");

            //var existingFile = new FileInfo(filePath);
            //using (ExcelPackage = new ExcelPackage(existingFile))
            //{
            //    sheet = ExcelPackage.Workbook.Worksheets.Copy("ItemTemplate", "Result");
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                ExcelPackage = new ExcelPackage(stream);
                sheet = ExcelPackage.Workbook.Worksheets.Copy("ItemTemplate", "Result");
                totalSheet = ExcelPackage.Workbook.Worksheets["TotalTemplate"];
            }
        }

        private void BuilTitle(string title)
        {
            sheet.Cells["A1:I1"].Value = title;
        }

        private void ClearTemplate()
        {
            ExcelPackage.Workbook.Worksheets.Delete("ItemTemplate");
            ExcelPackage.Workbook.Worksheets.Delete("TotalTemplate");
        }
    }
}