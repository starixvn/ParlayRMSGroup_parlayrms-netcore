using MediatR;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Controllers;
using ParlayRMS.WinLossRiskControl.Requests;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Controllers;

public class WinLossControllerUnitTests
{
    [Fact]
    public async Task GetSite_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossController(mediator);
        await controller.GetSite(new WinLossRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<WinLossRequest>());
    }

    [Fact]
    public async Task GetAgent_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossController(mediator);
        await controller.GetAgent(new AgentWinLossRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<AgentWinLossRequest>());
    }

    [Fact]
    public async Task GetCompany_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new WinLossController(mediator);
        await controller.GetCompany(new SuperWinLossRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<SuperWinLossRequest>());
    }
}