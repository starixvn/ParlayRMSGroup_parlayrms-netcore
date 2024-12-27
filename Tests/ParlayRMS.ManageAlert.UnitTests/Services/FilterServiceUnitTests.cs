using Fanex.RemotingRef;
using NSubstitute;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Services;

public class FilterServiceUnitTests
{
    private readonly FilterService filterService;

    public FilterServiceUnitTests()
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