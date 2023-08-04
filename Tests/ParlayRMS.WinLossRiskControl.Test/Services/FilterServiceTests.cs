using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Fanex.Data.Repository;
using Fanex.RemotingRef;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Services;
using Xunit;

namespace ParlayRMS.WinLossRiskControl.Test.Services;

public class FilterServiceTests
{
	private readonly IDynamicRepository repository;
	private readonly IRemotingRefService remotingRefService;
	private readonly FilterService service;

	public FilterServiceTests()
	{
		repository = Substitute.For<IDynamicRepository>();
		remotingRefService = Substitute.For<IRemotingRefService>();
		service = new FilterServiceImp(repository, remotingRefService);
	}

	private static List<SelectItemModel> ExpectedListParlayTypes()
	{
		return new List<SelectItemModel>
		{
			new() { Name = "Mix Parlay", Id = 1 },
			new() { Name = "God Of Fortune", Id = 5 },
			new() { Name = "Combo Parlay", Id = 2 },
			new() { Name = "Penalty Shootout Combo", Id = 4 },
			new() { Name = "Single Match Parlay", Id = 3 }
		};
	}

	[Fact]
	public async Task GetParlayTypes_Always_ReturnsExpectedList()
	{
		// Act
		var actualResults = await service.GetParlayTypes();

		// Assert
		var expectedList = ExpectedListParlayTypes();
		AssertExtension.AreEqual(expectedList, actualResults);
	}

	[Fact]
	public async Task GetSports_Always_ReturnsExpectedTotalItems()
	{
		// Act
		var actualResults = await service.GetSports();

		// Assert
		const int expectedItems = 17;
		Assert.Equal(expectedItems, actualResults.Count);
	}

	[Fact]
	public async Task GetCompany_Always_ReturnNotNull()
	{
		// Arrange
		var fixture = new Fixture();
		var company = fixture.CreateMany<CompanyModel>(2).ToList();
		repository.FetchAsync<CompanyModel>(Arg.Any<CompanyCriteria>()).Returns(company);

		// Act
		var actualResult = await service.GetCompany();

		// Assert
		Assert.NotNull(actualResult);
		Assert.Equal(2, actualResult.Count);
	}
}