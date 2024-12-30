using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using ParlayRMS.ManageAlert.Handlers;
using ParlayRMS.ManageAlert.Models;
using ParlayRMS.ManageAlert.Requests;
using ParlayRMS.ManageAlert.Services;

namespace ParlayRMS.ManageAlert.UnitTests.Handlers;

public class ViewLogHandlerUnitTests
{
    [Fact]
    public async Task Handle_Always_ReturnExpectedLogs()
    {
        // Arrange
        var service = Substitute.For<ManageAlertService>();
        service.GetViewLogs(Arg.Any<ViewLogRequest>()).Returns(DataHelper.GetViewLogs());
        var viewLogHandler = new ViewLogHandler(service);

        // Act

        var actualResults = await viewLogHandler.Handle(new ViewLogRequest(), CancellationToken.None);

        // Assert
        Assert.Single(actualResults);
    }
}