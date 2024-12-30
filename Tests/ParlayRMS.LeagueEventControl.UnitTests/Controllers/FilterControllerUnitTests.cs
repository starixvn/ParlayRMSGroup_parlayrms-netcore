using MediatR;
using NSubstitute;
using ParlayRMS.LeagueEventControl.Controllers;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl.UnitTests.Controllers;

public class FilterControllerUnitTests
{
    [Fact]
    public async Task League_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new FilterController(mediator);
        await controller.League(new LeagueRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<LeagueRequest>());
    }

    [Fact]
    public async Task Bettype_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new FilterController(mediator);
        await controller.Bettype(new BettypeRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<BettypeRequest>());
    }
}