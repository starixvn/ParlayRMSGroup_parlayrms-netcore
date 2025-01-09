using AutoMapper;
using NSubstitute;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Handlers;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Handlers;

public class ViewDetailHandlerUnitTests
{
    private readonly IMapper mapper;

    private readonly ManageAlertService service;

    public ViewDetailHandlerUnitTests()
    {
        mapper = new MapperConfiguration(cf =>
        {
            cf.CreateMap<PotentialWinningDetailRequest, ViewDetailCriteria>();
            cf.CreateMap<SimilarTicketDetailRequest, ViewDetailCriteria>();
        }).CreateMapper();
        service = Substitute.For<ManageAlertService>();
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedPotentialWinningDetails()
    {
        // Arrange
        service.GetPotentialWinningDetail(Arg.Any<ViewDetailCriteria>()).Returns(DataHelper.GetPotentialWinningDetails());
        var handler = new PotentialWinningDetailHandler(service, mapper);

        // Act
        var actualResults = await handler.Handle(new PotentialWinningDetailRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actualResults);
    }

    [Fact]
    public async Task Handle_Always_ReturnExpectedSimilarTicketDetails()
    {
        // Arrange
        service.GetSimilarTicketDetail(Arg.Any<ViewDetailCriteria>()).Returns(DataHelper.GetSimilarTicketDetails());
        var handler = new SimilarTicketDetailHandler(service, mapper);

        // Act
        var actualResults = await handler.Handle(new SimilarTicketDetailRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actualResults);
    }
}