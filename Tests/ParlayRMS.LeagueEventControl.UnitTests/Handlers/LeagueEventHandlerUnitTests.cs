using NSubstitute;
using ParlayRMS.LeagueEventControl.Handlers;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Handlers;

public class LeagueEventHandlerUnitTests
{
    private readonly LeagueEventService service = Substitute.For<LeagueEventService>();

    [Fact]
    public async Task Handler_Always_ReturnExpectedLeaguesData()
    {
        // Arrange
        service.GetLeaguesData(Arg.Any<LeagueDataRequest>()).Returns(Task.FromResult(DataHelper.GetLeagueData()));

        // Act
        var actualResults = await new LeagueEventHandler(service).Handle(new LeagueDataRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(2, actualResults.Count());
    }
}