using MediatR;
using NSubstitute;
using ParlayRMS.ManageAlert.Controllers;
using ParlayRMS.ManageAlert.Requests;

namespace ParlayRMS.ManageAlert.UnitTests.Controllers;

public class ManageAlertControllerUnitTests
{
    [Fact]
    public async Task Alerts_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.Alerts(new ManageAlertRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<ManageAlertRequest>());
    }

    [Fact]
    public async Task Detail_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.Detail(new SimilarTicketDetailRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<SimilarTicketDetailRequest>());
    }

    [Fact]
    public async Task PotentialWinningDetail_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.PotentialWinningDetail(new PotentialWinningDetailRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<PotentialWinningDetailRequest>());
    }

    [Fact]
    public async Task Logs_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.Logs(new ViewLogRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<ViewLogRequest>());
    }

    [Fact]
    public async Task TakeAction_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.TakeAction(new TakeActionRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<TakeActionRequest>());
    }

    [Fact]
    public async Task ExportExcelForAlerts_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.ExportExcelForAlerts(new ManageAlertExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<ManageAlertExcelRequest>());
    }

    [Fact]
    public async Task ExportExcelForSimilarBet_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.ExportExcelForSimilarBet(new SimilarTicketDetailExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<SimilarTicketDetailExcelRequest>());
    }

    [Fact]
    public async Task ExportExcelForPotentialWinning_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new ManageAlertController(mediator);
        await controller.ExportExcelForPotentialWinning(new PotentialWinningDetailExcelRequest());

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<PotentialWinningDetailExcelRequest>());
    }
}