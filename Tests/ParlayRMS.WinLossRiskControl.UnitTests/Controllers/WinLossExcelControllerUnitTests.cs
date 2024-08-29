using MediatR;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Controllers;
using ParlayRMS.WinLossRiskControl.Requests.Excels;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Controllers;

public class WinLossExcelControllerUnitTests
{
    [Fact]
    public async Task ExcelSite_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossExcelController(mediator);
        await controller.ExcelSite(new SiteWinLossExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<SiteWinLossExcelRequest>());
    }

    [Fact]
    public async Task ExcelCompany_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossExcelController(mediator);
        await controller.ExcelCompany(new SuperWinLossExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<SuperWinLossExcelRequest>());
    }

    [Fact]
    public async Task ExcelAgent_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossExcelController(mediator);
        await controller.ExcelAgent(new AgentWinLossExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<AgentWinLossExcelRequest>());
    }

    [Fact]
    public async Task ExcelMember_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossExcelController(mediator);
        await controller.ExcelMember(new MemberWinLossExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<MemberWinLossExcelRequest>());
    }
}