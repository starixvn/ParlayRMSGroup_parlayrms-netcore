using AutoFixture;
using Fanex.Data.Repository;
using Fanex.RemotingRef;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Services;

public class FilterServiceUnitTests
{
    private readonly IDynamicRepository repository;
    private readonly FilterService filterService;

    public FilterServiceUnitTests()
    {
        repository = Substitute.For<IDynamicRepository>();
        var remotingRefService = Substitute.For<IRemotingRefService>();
        filterService = new FilterServiceImp(repository, remotingRefService);
    }

    [Fact]
    public async Task GetParlayTypes_Always_ReturnsExpectedTotalItems()
    {
        // Act
        var actualResults = await filterService.GetParlayTypes();

        // Assert
        const int expectedItems = 6;
        Assert.Equal(expectedItems, actualResults.Count);
    }

    [Fact]
    public async Task GetCompany_Always_ReturnNotNull()
    {
        // Arrange
        repository.FetchAsync<CompanyModel>(Arg.Any<CompanyCriteria>()).Returns(DataHelper.GetCompany());

        // Act
        var actualResult = await filterService.GetCompany();

        // Assert
        Assert.NotNull(actualResult);
        Assert.Equal(2, actualResult.Count);
    }

    [Fact]
    public async Task GetSports_Always_ReturnsExpectedTotalItems()
    {
        // Act
        var actualResults = await filterService.GetSports();

        // Assert
        const int expectedItems = 18;
        Assert.Equal(expectedItems, actualResults.Count);
    }

    [Fact]
    public async Task GetComboes_Always_ReturnsExpectedTotalItems()
    {
        // Act
        var actualResults = await filterService.GetComboes();

        // Assert
        const int expectedItems = 10;
        Assert.Equal(expectedItems, actualResults.Count);
    }
}