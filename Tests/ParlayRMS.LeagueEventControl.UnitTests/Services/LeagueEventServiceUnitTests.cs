using AutoMapper;
using Fanex.Data.Repository;
using NSubstitute;
using ParlayRMS.LeagueEventControl.Criteria;
using ParlayRMS.LeagueEventControl.Models;
using ParlayRMS.LeagueEventControl.Requests;
using ParlayRMS.LeagueEventControl.Services;

namespace ParlayRMS.LeagueEventControl.UnitTests.Services;

public class LeagueEventServiceUnitTests
{
    private readonly IDynamicRepository repository;

    private readonly IMapper mapper;

    public LeagueEventServiceUnitTests()
    {
        repository = Substitute.For<IDynamicRepository>();
        mapper = new MapperConfiguration(cf => { cf.AddProfile(new LeagueEventMappingProfile()); }).CreateMapper();
    }

    [Fact]
    public async Task GetLeaguesData_Always_ReturnExpectedLeagues()
    {
        // Arrange
        repository.FetchAsync<LeagueDataModel>(Arg.Any<LeagueEventCriteria>()).Returns(DataHelper.GetLeagueData());
        var service = new LeagueEventServiceImp(repository, mapper);

        // Act
        var actualResults = await service.GetLeaguesData(new LeagueDataRequest());

        // Assert
        Assert.Equal(2, actualResults.Count());
    }

    [Fact]
    public async Task GetEvents_Always_ReturnExpectedEvents()
    {
        // Arrange
        repository.Fetch<EventModel>(Arg.Any<LeagueEventCriteria>()).Returns(DataHelper.GetEvents());
        var service = new LeagueEventServiceImp(repository, mapper);

        // Act
        var actualResults = await service.GetEvents(new EventRequest());

        // Assert
        Assert.Equal(1, actualResults.Count());
        Assert.Equal(2, actualResults.First().EventList.Count);
        Assert.Equal(2, actualResults.First().RowsTotal);
        Assert.Equal(24.5552m, actualResults.First().TotalStake);
    }

    [Fact]
    public async Task GetEventDetail_Always_ReturnExpectedEventDetail()
    {
        // Arrange
        repository.Fetch<EventDetailModel>(Arg.Any<EventDetailCriteria>()).Returns(DataHelper.GetEventsDetail());
        var service = new LeagueEventServiceImp(repository, mapper);

        // Act
        var actualResults = await service.GetEventDetail(new EventDetailRequest());

        // Assert
        Assert.Equal(2, actualResults.Count);
        Assert.Equal(null, actualResults.First().Market);
    }

    [Fact]
    public async Task GetMainTickets_Always_ReturnExpectedLeagues()
    {
        // Arrange
        repository.Fetch<MainTicketModel>(Arg.Any<EventDetailCriteria>()).Returns(DataHelper.GetMainTicket());
        var service = new LeagueEventServiceImp(repository, mapper);

        // Act
        var actualResults = await service.GetMainTickets(new ExcelRequest());

        // Assert
        Assert.Single(actualResults);
    }
}