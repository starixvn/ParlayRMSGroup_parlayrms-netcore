using Fanex.RemotingRef;
using NSubstitute;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Controllers;

public class FilterControllerUnitTests
{
    private readonly FilterService filterService;

    public FilterControllerUnitTests()
    {
        var remotingRefService = Substitute.For<IRemotingRefService>();
        filterService = new FilterServiceImp(remotingRefService);
    }
    [Fact]
    public void GetSportTypes_Always_ReturnsExpectedTotalItems()
    {
        // Act
        var actualResults = filterService.GetSportTypes();

        // Assert
        const int expectedItems = 18;
        Assert.Equal(expectedItems, actualResults.Count());
    }
}