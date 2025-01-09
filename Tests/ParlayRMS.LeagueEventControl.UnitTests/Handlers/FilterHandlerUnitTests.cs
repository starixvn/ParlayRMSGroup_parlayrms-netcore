using NSubstitute;
using ParlayRMS.LeagueEventControl.Handlers;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Handlers;

public class FilterHandlerUnitTests
{
    private readonly FilterService service = Substitute.For<FilterService>();

    [Fact]
    public async Task Handle_Always_ReturnExpectedBettypes()
    {
        // Arrange
        service.GetBettypes(Arg.Any<BettypeRequest>()).Returns(Task.FromResult(DataHelper.GetBettypes()));

        // Act
        var actualResults = await new FilterHandler(service).Handle(new BettypeRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(2, actualResults.Count());
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedLeagues()
    {
        // Arrange
        service.GetLeagues(Arg.Any<LeagueRequest>()).Returns(Task.FromResult(DataHelper.GetLeagues()));

        // Act
        var actualResults = await new FilterHandler(service).Handle(new LeagueRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actualResults);
    }
}