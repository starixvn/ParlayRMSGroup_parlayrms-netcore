using AutoMapper;
using NSubstitute;
using ParlayRMS.ManageAlert.Criteria;
using ParlayRMS.ManageAlert.Handlers;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Handlers;

public class ManageAlertHandlerUnitTests
{
    [Fact]
    public async Task Handler_ManageAlert_ReturnExpectedList()
    {
        // Arrange
        var mapper = new MapperConfiguration(cf =>
        {
            cf.CreateMap<ManageAlertRequest, ManageAlertCriteria>();
        }).CreateMapper();
        var manageAlert = new ManageAlertViewModel
        {
            Items = DataHelper.GetManageAlerts(),
            TotalItems = 1
        };
        var service = Substitute.For<ManageAlertService>();
        service.GetAlerts(Arg.Any<ManageAlertCriteria>()).Returns(manageAlert);
        var handler = new ManageAlertHandler(service, mapper);

        // Act
        var actionResult = await handler.Handle(new ManageAlertRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actionResult.Items);
        Assert.Equal(1, actionResult.TotalItems);
    }
}