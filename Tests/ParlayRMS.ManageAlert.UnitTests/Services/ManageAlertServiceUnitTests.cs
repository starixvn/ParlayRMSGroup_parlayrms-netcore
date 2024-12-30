using AutoMapper;
using Fanex.Data.Repository;
using NSubstitute;
using ParlayRMS.ManageAlert.AutoMapperProfiles;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Services;

public class ManageAlertServiceUnitTests
{
    private readonly IDynamicRepository repository;
    private readonly ManageAlertService service;

    public ManageAlertServiceUnitTests()
    {
        repository = Substitute.For<IDynamicRepository>();
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ManageAlertMappingProfile());
        });
        var mapper = mockMapper.CreateMapper();
        service = new ManageAlertServiceImp(repository, mapper);
    }

    [Fact]
    public async Task GetAlerts_Always_ReturnExpectedAlerts()
    {
        // Arrange
        var criteria = new ManageAlertCriteria
            { FromDate = "", ToDate = "", ActionType = "", AlertType = "", Selection = "", TicketStatus = "" };
        repository.FetchMultipleAsync<ManageAlertModel, int>(Arg.Any<ManageAlertCriteria>()).Returns((DataHelper.GetManageAlerts(), [1]));

        // Act
        var actualResults = await service.GetAlerts(criteria);

        // Assert
        Assert.Equal(1, actualResults.TotalItems);
        Assert.Single(actualResults.Items);
    }

    [Fact]
    public async Task GetSimilarTicketDetail_Always_ReturnExpectedList()
    {
        // Arrange
        var criteria = new ViewDetailCriteria { Selection = "", TicketStatus = "" };
        repository.FetchAsync<SimilarTicketDetailModel>(criteria).Returns(DataHelper.GetSimilarTicketDetails());

        // Act
        var actualResults = await service.GetSimilarTicketDetail(criteria);

        // Assert
        Assert.Single(actualResults);
    }

    [Fact]
    public async Task GetPotentialWinningDetail_Always_ReturnExpectedList()
    {
        // Arrange
        var criteria = new ViewDetailCriteria { Selection = "", TicketStatus = "" };
        repository.FetchAsync<PotentialWinningDetailModel>(criteria).Returns(DataHelper.GetPotentialWinningDetails());

        // Act
        var actualResults = await service.GetPotentialWinningDetail(criteria);

        // Assert
        Assert.Single(actualResults);
    }

    [Fact]
    public async Task GetViewLogs_Always_ReturnExpectedList()
    {
        // Arrange
        var request = new ViewLogRequest();
        repository.FetchAsync<ViewLogModel>(Arg.Any<ViewLogCriteria>()).Returns(DataHelper.GetViewLogs());

        // Act
        var actualResults = await service.GetViewLogs(request);

        // Assert
        Assert.Single(actualResults);
    }

    [Fact]
    public async Task TakeAction_Always_ReturnExpectedList()
    {
        // Arrange
        var request = new TakeActionRequest();
        repository.ExecuteAsync(Arg.Any<TakeActionCriteria>()).Returns(1);

        // Act
        var actualResults = await service.TakeAction(request);

        // Assert
        Assert.Equal(1, actualResults);
    }
}