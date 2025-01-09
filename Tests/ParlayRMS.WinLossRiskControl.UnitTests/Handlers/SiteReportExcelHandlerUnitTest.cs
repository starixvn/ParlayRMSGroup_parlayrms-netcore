using AutoMapper;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Requests.Excels;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Handlers;

public class SiteReportExcelHandlerUnitTest
{
    private readonly IMapper mapper = new MapperConfiguration(cf =>
    {
        cf.AddProfile(new WinLossMappingProfile());
    }).CreateMapper();

    private readonly WinLossService service = Substitute.For<WinLossService>();

    [Fact]
    public async Task Handle_Always_ReturnExpectedSiteWinLossExcelPackage()
    {
        // Arrange
        var excelService = new SiteExcelServiceImp(mapper);
        service.SitesWinLoss(Arg.Any<WinLossRequest>())
            .Returns(Task.FromResult(DataHelper.ExpectedSiteWinLoss()));

        // Act
        var excelPackage =
            await new SiteReportExcelHandler(service, mapper, excelService).Handle(new SiteWinLossExcelRequest(), CancellationToken.None);

        // Assert
        Assert.Equal("Site/Username", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value);
        Assert.Equal("MBC Group", excelPackage.Workbook.Worksheets[0].Cells["A3"].Value);
        Assert.Equal("Licensee Group", excelPackage.Workbook.Worksheets[0].Cells["A5"].Value);
        Assert.Equal("Grand Total", excelPackage.Workbook.Worksheets[0].Cells["A7"].Value);
    }
}