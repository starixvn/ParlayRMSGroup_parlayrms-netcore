using System;
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
        ExcelPackage Build(string title, AgentWinLossModel model);
    }

    public class WinLossExcelServiceImp : WinLossExcelService
    {
        private const int StartRowIndex = 3;

        private readonly IMapper mapper;

        private ExcelWorksheet sheet;

        private ExcelWorksheet totalSheet;

        private ExcelPackage ExcelPackage { get; set; }

        public WinLossExcelServiceImp(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ExcelPackage Build(string title, AgentWinLossModel model)
        {
            LoadTemplate();
            BuilTitle(title);
            BuildRow(model.Rows);
            BuildTotal(model);
            ClearTemplate();
            return ExcelPackage;
        }

        private void BuildRow(List<WinLossModel> model)
        {
            var excelData = mapper.Map<List<MemberWinLossExcelModel>>(model);
            sheet.ApplyVertical(excelData, StartRowIndex);
        }

        private void BuildTotal(AgentWinLossModel model)
        {
            var excelData = mapper.Map<MemberWinLossExcelModel>(model);
            var totalIndex = StartRowIndex + model.Rows.Count;
            sheet.ApplyModel(totalSheet, excelData, totalIndex);
        }

        private void LoadTemplate()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "ExcelTemplate", "WinLossRiskControl", "member-win-loss.xlsx");

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