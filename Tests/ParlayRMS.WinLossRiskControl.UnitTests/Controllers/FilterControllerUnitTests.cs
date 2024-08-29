using MediatR;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Controllers;
using ParlayRMS.WinLossRiskControl.Requests;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Controllers;

public class FilterControllerUnitTests
{
    [Fact]
    public async Task Sports_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new FilterController(mediator);
        await controller.Sports();

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<SportRequest>());
    }

    [Fact]
    public async Task ParlayTypes_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new FilterController(mediator);
        await controller.ParlayTypes();

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<ParlayTypeRequest>());
    }

    [Fact]
    public async Task Company_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new FilterController(mediator);
        await controller.Company();

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<CompanyRequest>());
    }

    [Fact]
    public async Task Comboes_Always_CallMediatorSend()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();

        // Act
        var controller = new FilterController(mediator);
        await controller.Comboes();

        // Assert
        await mediator.Received().Send(Arg.Compat.Any<ComboRequest>());
    }
}