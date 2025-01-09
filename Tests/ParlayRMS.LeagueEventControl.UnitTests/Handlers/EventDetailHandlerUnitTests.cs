using NSubstitute;
using ParlayRMS.LeagueEventControl.Handlers;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Handlers;

public class EventDetailHandlerUnitTests
{
    [Fact]
    public async Task Handle_Always_ReturnExpectedEventDetail()
    {
        // Arrange
        var service = Substitute.For<LeagueEventService>();
        service.GetEventDetail(Arg.Any<EventDetailRequest>()).Returns(Task.FromResult(DataHelper.GetEventsDetail().ToList()));
        var handler = new EventDetailHandler(service);

        // Act

        var actualResults = await handler.Handle(new EventDetailRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actualResults);
    }
}