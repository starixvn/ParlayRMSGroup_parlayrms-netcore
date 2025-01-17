using AutoMapper;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Models.Excels;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Services;

public class SiteExcelServiceUnitTests
{
    private readonly SiteExcelServiceImp service;

    public SiteExcelServiceUnitTests()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<WinLossModel, MemberWinLossExcelModel>();
            cfg.CreateMap<WinLossModel, WinLossExcelModel>();
            cfg.CreateMap<WinLossModel, SiteWinLossExcelModel>();
        });

        var mapper = mockMapper.CreateMapper();
        service = new SiteExcelServiceImp(mapper);
    }

    [Fact]
    public void CreateExcelPackage_ValidModelAndSettings_ReturnsExcelPackage()
    {
        // Arrange
        var model = new WinLossModel
        {
            NoMember = 13,
            Username = "Grand Total",
            Rows =
            [
                new WinLossModel { IsDeposit = false, NoMember = 11, Username = "MBC Group", Rows = [
                    new WinLossModel { IsDeposit = false, NoMember = 1, Username = "222223"},
                    new WinLossModel { IsDeposit = false, NoMember = 1, Username = "733333"}
                ]},
                new WinLossModel { IsDeposit = true, NoMember = 2, Username = "Licensee Group", Rows = [
                    new WinLossModel { IsDeposit = true, NoMember = 1, Username = "12Bet"},
                    new WinLossModel { IsDeposit = true, NoMember = 1, Username = "365online"}
                ]}
            ]
        };
        var settings = new ExcelSetting
        {
            SheetName = "Result",
            Title = "Site",
            RoleId = 0
        };

        // Act
        var result = service.CreateExcelPackage(model, settings);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Site", result.Workbook.Worksheets["Result"].Cells["A1"].Value);
        Assert.Equal("Grand Total", result.Workbook.Worksheets["Result"].Cells["A9"].Value);
    }
}