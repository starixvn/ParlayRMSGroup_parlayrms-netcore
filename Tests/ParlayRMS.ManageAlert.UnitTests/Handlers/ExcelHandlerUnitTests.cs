using AutoMapper;
using NSubstitute;
using ParlayRMS.ManageAlert.AutoMapperProfiles;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Excels;
using ParlayRMS.ManageAlert.Handlers;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Handlers;

public class ExcelHandlerUnitTests
{
    private readonly ExcelBuilder excelBuilder;

    private readonly ManageAlertService service;

    private readonly IMapper mapper;

    public ExcelHandlerUnitTests()
    {
        excelBuilder = new ExcelBuilderImp();
        service = Substitute.For<ManageAlertService>();
        mapper = new MapperConfiguration(cf =>
        {
            cf.AddProfile(new ManageAlertMappingProfile());
        }).CreateMapper();
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedManageAlertExcelPackage()
    {
        // Arrange
        var data = new ManageAlertViewModel()
        {
            TotalItems = 1,
            Items = DataHelper.GetManageAlerts()
        };
        service.GetAlerts(Arg.Any<ManageAlertCriteria>()).Returns(data);
        var handler = new ExcelHandler(excelBuilder, mapper, service);

        // Act
        var excelPackage = await handler.Handle(new ManageAlertExcelRequest(), CancellationToken.None);

        // Assert
        Assert.Equal("Manage_Alert", excelPackage.Workbook.Worksheets[0].Name);
        Assert.Equal("123456", excelPackage.Workbook.Worksheets[0].Cells["C2"].Value);
        Assert.Equal("1", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value.ToString());
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedSimilarTicketDetailExcelPackage()
    {
        // Arrange
        service.GetSimilarTicketDetail(Arg.Any<ViewDetailCriteria>()).Returns(DataHelper.GetSimilarTicketDetails());
        var handler = new ExcelHandler(excelBuilder, mapper, service);

        // Act
        var excelPackage = await handler.Handle(new SimilarTicketDetailExcelRequest(), CancellationToken.None);

        // Assert
        Assert.Equal("Similar_Bet", excelPackage.Workbook.Worksheets[0].Name);
        Assert.Equal("123456789", excelPackage.Workbook.Worksheets[0].Cells["B2"].Value);
        Assert.Equal("1", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value.ToString());
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedPotentialWinningDetailExcelPackage()
    {
        // Arrange
        service.GetPotentialWinningDetail(Arg.Any<ViewDetailCriteria>()).Returns(DataHelper.GetPotentialWinningDetails());
        var handler = new ExcelHandler(excelBuilder, mapper, service);

        // Act
        var excelPackage = await handler.Handle(new PotentialWinningDetailExcelRequest(), CancellationToken.None);

        // Assert
        Assert.Equal("Potential_Winning", excelPackage.Workbook.Worksheets[0].Name);
        Assert.Equal("123456789", excelPackage.Workbook.Worksheets[0].Cells["C2"].Value);
        Assert.Equal("1", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value.ToString());
    }
}