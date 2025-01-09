using NSubstitute;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Handlers;

public class EventHandlerUnitTests
{
    [Fact]
    public async Task Handle_Always_ReturnExpectedEvent()
    {
        // Arrange
        var service = Substitute.For<LeagueEventService>();
        service.GetEvents(Arg.Any<EventRequest>()).Returns(Task.FromResult(DataHelper.GetEventGroup()));
        var handler = new LeagueEventControl.Handlers.EventHandler(service);

        // Act

        var actualResults = await handler.Handle(new EventRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actualResults);
        Assert.Equal(2, actualResults.First().EventList.Count);
    }
}