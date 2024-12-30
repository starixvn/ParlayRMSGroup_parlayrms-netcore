using Fanex.RemotingRef;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ParlayRMS.ManageAlert.Controllers;
using ParlayRMS.ManageAlert.Models;
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
        var actualResults = new FilterController(filterService).GetSports() as OkObjectResult;

        // Assert
        Assert.Equal(18, ((actualResults!.Value as List<SportModel>)!).Count);
    }
}