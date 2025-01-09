using MediatR;
using NSubstitute;
using ParlayRMS.LeagueEventControl.Controllers;
using ParlayRMS.LeagueEventControl.Requests;

namespace ParlayRMS.LeagueEventControl.UnitTests.Controllers;

public class LeagueEventControllerUnitTests
{
    [Fact]
    public async Task League_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new LeagueEventController(mediator);
        await controller.League(new LeagueDataRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<LeagueDataRequest>());
    }

    [Fact]
    public async Task Event_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new LeagueEventController(mediator);
        await controller.Event(new EventRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<EventRequest>());
    }

    [Fact]
    public async Task EventDetail_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new LeagueEventController(mediator);
        await controller.EventDetail(new EventDetailRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<EventDetailRequest>());
    }

    [Fact]
    public async Task ExportExcel_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new LeagueEventController(mediator);
        await controller.ExportExcel(new ExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<ExcelRequest>());
    }
}