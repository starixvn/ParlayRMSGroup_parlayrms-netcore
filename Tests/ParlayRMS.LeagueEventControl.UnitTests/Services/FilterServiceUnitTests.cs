using AutoMapper;
using Fanex.Data.Repository;
using NSubstitute;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Services;

public class FilterServiceUnitTests
{
    private readonly IDynamicRepository repository;

    private readonly IMapper mapper;

    public FilterServiceUnitTests()
    {
        repository = Substitute.For<IDynamicRepository>();
        mapper = new MapperConfiguration(cf =>
        {
            cf.AddProfile(new LeagueEventMappingProfile());
        }).CreateMapper();
    }

    [Fact]
    public async Task GetLeagues_Always_ReturnExpectedLeagues()
    {
        // Arrange
        repository.FetchAsync<LeagueModel>(Arg.Any<LeagueCriteria>()).Returns(DataHelper.GetLeagues());
        var filterService = new FilterServiceImp(repository, mapper);

        // Act
        var actualResults = await filterService.GetLeagues(new LeagueRequest());

        // Assert
        Assert.Single(actualResults);
    }

    [Fact]
    public async Task GetBettypes_Always_ReturnExpectedBettypes()
    {
        // Arrange
        repository.FetchAsync<BettypeModel>(Arg.Any<BettypeCriteria>()).Returns(DataHelper.GetBettypes());
        var filterService = new FilterServiceImp(repository, mapper);

        // Act
        var actualResults = await filterService.GetBettypes(new BettypeRequest());

        // Assert
        Assert.Equal(2, actualResults.Count());
    }
}