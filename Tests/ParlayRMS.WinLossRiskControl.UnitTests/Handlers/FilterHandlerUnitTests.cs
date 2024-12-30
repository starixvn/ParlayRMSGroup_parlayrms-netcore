using Fanex.Data.Repository;
using Fanex.RemotingRef;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Handlers;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;

namespace ParlayRMS.WinLossRiskControl.UnitTests.Handlers;

public class FilterHandlerUnitTests
{
    [Fact]
    public async Task Handle_Always_ReturnExpectedSports()
    {
        // Arrange
        var service = new FilterServiceImp(Substitute.For<IDynamicRepository>(), Substitute.For<IRemotingRefService>());

        // Act
        var actionResults = await new FilterHandler(service).Handle(new SportRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(18, actionResults.Count);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedParlayTypes()
    {
        // Arrange
        var service = new FilterServiceImp(Substitute.For<IDynamicRepository>(), Substitute.For<IRemotingRefService>());

        // Act
        var actionResults = await new FilterHandler(service).Handle(new ParlayTypeRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(5, actionResults.Count);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedComboes()
    {
        // Arrange
        var service = new FilterServiceImp(Substitute.For<IDynamicRepository>(), Substitute.For<IRemotingRefService>());

        // Act
        var actionResults = await new FilterHandler(service).Handle(new ComboRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(9, actionResults.Count);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedCompany()
    {
        // Arrange
        var repository = Substitute.For<IDynamicRepository>();
        repository.FetchAsync<CompanyModel>(Arg.Any<CompanyCriteria>()).Returns(DataHelper.GetCompany());
        var service = new FilterServiceImp(repository, Substitute.For<IRemotingRefService>());

        // Act
        var actionResults = await new FilterHandler(service).Handle(new CompanyRequest(), CancellationToken.None);

        // Assert
        Assert.Equal(2, actionResults.Count);
    }
}