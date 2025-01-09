using NSubstitute;
using ParlayRMS.ManageAlert.Handlers;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Handlers;

public class TakeActionHandlerUnitTests
{
    [Fact]
    public async Task Handler_Always_ReturnExpectedTakeActions()
    {
        // Arrange
        var service = Substitute.For<ManageAlertService>();
        service.TakeAction(Arg.Any<TakeActionRequest>()).Returns(1);
        var handler = new TakeActionHandler(service);

        // Act
        var actualResult = await handler.Handle(new TakeActionRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(1, actualResult);
    }
}