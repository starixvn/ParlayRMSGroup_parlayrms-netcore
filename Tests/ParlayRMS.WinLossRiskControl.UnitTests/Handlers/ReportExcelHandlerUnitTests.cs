using AutoMapper;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Requests.Excels;
using ParlayRMS.WinLossRiskControl.Services;
using ParlayRMS.WinLossRiskControl.Shared;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Handlers;

public class ReportExcelHandlerUnitTests
{
    private readonly WinLossExcelBuilder builder = new WinLossExcelBuilderImp();

    private readonly IMapper mapper = new MapperConfiguration(cf =>
    {
        cf.AddProfile(new WinLossMappingProfile());
    }).CreateMapper();

    private readonly WinLossService service = Substitute.For<WinLossService>();

    [Fact]
    public async Task Handle_Always_ReturnExpectedSuperWinLossExcelPackage()
    {
        // Arrange
        service.SuperWinLoss(Arg.Any<SuperWinLossRequest>())
            .Returns(Task.FromResult(DataHelper.ExpectedCompanyWinLoss()));

        // Act
        var excelPackage =
            await new ReportExcelHandler(service, mapper, builder).Handle(new SuperWinLossExcelRequest() { Company = "222223" },
                CancellationToken.None);

        // Assert
        Assert.Equal("Super", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value);
        Assert.Equal("Grand Total", excelPackage.Workbook.Worksheets[0].Cells["A7"].Value);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedAgentWinLossBySiteExcelPackage()
    {
        // Arrange
        service.AgentWinLossBySite(Arg.Any<AgentWinLossRequest>())
            .Returns(Task.FromResult(DataHelper.ExpectedAgentWinLoss()));

        // Act
        var excelPackage =
            await new ReportExcelHandler(service, mapper, builder).Handle(new AgentWinLossExcelRequest() { RoleId = 3 },
                CancellationToken.None);

        // Assert
        Assert.Equal("Agent", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value);
        Assert.Equal("Grand Total", excelPackage.Workbook.Worksheets[0].Cells["A6"].Value);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedAgentWinLossByMasterExcelPackage()
    {
        // Arrange
        service.AgentWinLossByMaster(Arg.Any<AgentWinLossRequest>())
            .Returns(Task.FromResult(DataHelper.ExpectedAgentWinLoss()));

        // Act
        var excelPackage =
            await new ReportExcelHandler(service, mapper, builder).Handle(new AgentWinLossExcelRequest() { RoleId = 3, CustId = 3},
                CancellationToken.None);

        // Assert
        Assert.Equal("Agent", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value);
        Assert.Equal("Grand Total", excelPackage.Workbook.Worksheets[0].Cells["A6"].Value);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedMemberWinLossExcelPackage()
    {
        // Arrange
        service.AgentWinLossByMaster(Arg.Any<AgentWinLossRequest>())
            .Returns(Task.FromResult(DataHelper.ExpectedAgentWinLoss()));

        // Act
        var excelPackage =
            await new ReportExcelHandler(service, mapper, builder).Handle(new MemberWinLossExcelRequest() { RoleId = 2 },
                CancellationToken.None);

        // Assert
        Assert.Equal("Member", excelPackage.Workbook.Worksheets[0].Cells["A2"].Value);
        Assert.Equal("Grand Total", excelPackage.Workbook.Worksheets[0].Cells["A6"].Value);
    }
}