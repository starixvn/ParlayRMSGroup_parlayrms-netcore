using NSubstitute;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Handlers;

public class ReportHandlerUnitTests
{
    private readonly WinLossService service = Substitute.For<WinLossService>();

    [Fact]
    public async Task Handle_Always_ReturnExpectedWinloss()
    {
        // Arrange
        service.SitesWinLoss(Arg.Any<WinLossRequest>()).Returns(Task.FromResult(DataHelper.ExpectedSiteWinLoss()));
        
        // Act
        var actualResults = await new ReportHandler(service).Handle(new WinLossRequest(), CancellationToken.None);

        // Assert
        AssertExtension.AreEqual(DataHelper.ExpectedSiteWinLoss(), actualResults);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedSuperWinLoss()
    {
        // Arrange
        service.SuperWinLoss(Arg.Any<SuperWinLossRequest>()).Returns(Task.FromResult(DataHelper.ExpectedCompanyWinLoss()));

        // Act
        var actualResults = await new ReportHandler(service).Handle(new SuperWinLossRequest(), CancellationToken.None);

        // Assert
        AssertExtension.AreEqual(DataHelper.ExpectedCompanyWinLoss(), actualResults);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedAgentWinLossBySite()
    {
        // Arrange
        var expectedList = new WinLossModel
        {
            Username = "Grand Total",
            NoMember = 3,
            BetCount = 6,
            MemberWinLoss = 9,
            SmaWinLoss = 12,
            ExtraPT = 15,
            Commission = 6,
            RealIncome = 17,
            RealTurnover = 22,
            Margin = Convert.ToDecimal("77.272727272727272727272727270"),
            Rows = DataHelper.AgentWinLoss
        };
        service.AgentWinLossBySite(Arg.Any<AgentWinLossRequest>()).Returns(Task.FromResult(expectedList));

        // Act
        var actualResults = await new ReportHandler(service).Handle(new AgentWinLossRequest(), CancellationToken.None);

        // Assert
        AssertExtension.AreEqual(expectedList, actualResults);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedAgentWinLossByMaster()
    {
        // Arrange
        service.AgentWinLossByMaster(Arg.Any<AgentWinLossRequest>()).Returns(Task.FromResult(DataHelper.ExpectedAgentWinLoss()));

        // Act
        var actualResults = await new ReportHandler(service).Handle(new AgentWinLossRequest() { CustId = 3 }, CancellationToken.None);

        // Assert
        AssertExtension.AreEqual(DataHelper.ExpectedAgentWinLoss(), actualResults);
    }
}