using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Fanex.Data.Repository;
using NSubstitute;
using ParlayRMS.WinLossRiskControl.Criteria;
using ParlayRMS.WinLossRiskControl.Models;
using ParlayRMS.WinLossRiskControl.Requests;
using ParlayRMS.WinLossRiskControl.Services;
using Xunit;

namespace ParlayRMS.WinLossRiskControl.Test.Services;

public class WinLossServiceTests
{
	private readonly IDynamicRepository repository;
	private readonly WinLossService service;

	public WinLossServiceTests()
	{
		var mockMapper = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile(new WinLossMappingProfile());
		});

		var mapper = mockMapper.CreateMapper();
		repository = Substitute.For<IDynamicRepository>();
		service = new WinLossServiceImp(repository, mapper);
	}

	private static WinLossModel CreateWinLoss(decimal commission, decimal realInCome, decimal realTunrover)
	{
		return new WinLossModel
		{
			Username = "A",
			NoMember = 1,
			BetCount = 2,
			MemberWinLoss = 3,
			SmaWinLoss = 4,
			ExtraPT = 5,
			Commission = commission,
			RealIncome = realInCome,
			RealTurnover = realTunrover
		};
	}

	private static WinLossModel CreateWinLoss(decimal commission, decimal realInCome, decimal realTunrover, decimal margin)
	{
		var model = CreateWinLoss(commission, realInCome, realTunrover);
		model.Margin = margin;

		return model;
	}

	private WinLossModel ExpectedAgentWinLoss()
	{
		return new WinLossModel
		{
			Username = "Grand Total",
			NoMember = 3,
			BetCount = 6,
			MemberWinLoss = 9,
			SmaWinLoss = 12,
			ExtraPT = 15,
			Commission = 6,
			RealIncome = 17,
			RealTurnover = 22,
			Margin = Convert.ToDecimal("77.272727272727272727272727270"),
			Rows = FakeAgentWinLoss,
			TotalRecords = 3
		};
	}

	private List<WinLossModel> FakeAgentWinLoss => new()
	{
		CreateWinLoss(1, 3, 4, 57),
		CreateWinLoss(2, 7, 8, Convert.ToDecimal(87.5)),
		CreateWinLoss(3, 7, 10, 70)
	};

	private WinLossModel ExpectedCompanyWinLoss()
	{
		return new WinLossModel
		{
			Username = "Grand Total",
			NoMember = 4,
			BetCount = 8,
			MemberWinLoss = 12,
			SmaWinLoss = 16,
			ExtraPT = 20,
			Commission = 10,
			RealIncome = 17,
			RealTurnover = 22,
			Margin = Convert.ToDecimal("77.272727272727272727272727270"),
			Rows = FakeCompanyWinLoss
		};
	}

	private List<WinLossModel> FakeCompanyWinLoss => new()
	{
		CreateWinLoss(1, 3, 4, 57),
		CreateWinLoss(2, 7, 8, Convert.ToDecimal(87.5)),
		CreateWinLoss(3, 7, 10, 70),
		CreateWinLoss(4, 0, 0, 0)
	};

	private WinLossModel ExpectedSiteWinLoss()
	{
		var credit = new WinLossModel
		{
			Username = "MBC Group",
			NoMember = 1,
			BetCount = 2,
			MemberWinLoss = 3,
			SmaWinLoss = 4,
			ExtraPT = 5,
			Commission = 1,
			RealIncome = 3,
			RealTurnover = 4,
			Margin = Convert.ToDecimal("75.00"),
			IsDeposit = false,
			Rows = new List<WinLossModel> { MBCGroup }
		};

		var deposit = new WinLossModel
		{
			Username = "Licensee Group",
			NoMember = 1,
			BetCount = 2,
			MemberWinLoss = 3,
			SmaWinLoss = 4,
			ExtraPT = 5,
			Commission = 3,
			RealIncome = 7,
			RealTurnover = 10,
			Margin = 70,
			IsDeposit = true,
			Rows = new List<WinLossModel> { LicenseeGroup }
		};

		return new WinLossModel
		{
			Username = "Grand Total",
			NoMember = 2,
			BetCount = 4,
			MemberWinLoss = 6,
			SmaWinLoss = 8,
			ExtraPT = 10,
			Commission = 4,
			RealIncome = 10,
			RealTurnover = 14,
			Margin = Convert.ToDecimal("71.428571428571428571428571430"),
			Rows = new List<WinLossModel> { credit, deposit }
		};
	}

	private WinLossModel MBCGroup
	{
		get
		{
			var credit = CreateWinLoss(1, 3, 4, 57);
			credit.IsDeposit = false;
			return credit;
		}
	}

	private WinLossModel LicenseeGroup
	{
		get
		{
			var deposit = CreateWinLoss(3, 7, 10, 70);
			deposit.IsDeposit = true;
			return deposit;
		}
	}

	private IEnumerable<WinLossModel> FakeSiteWinLoss =>
		new List<WinLossModel> {
			MBCGroup,
			LicenseeGroup
		};

	[Fact]
	public async Task GetSites_Always_ReturnsExpectedList()
	{
		// Arrange
		var request = new WinLossRequest();
		var fakeData = FakeSiteWinLoss;
		repository.FetchAsync<WinLossModel>(Arg.Any<WinLossCriteria>()).Returns(fakeData);

		// Act
		var actualResults = await service.SitesWinLoss(request);

		// Assert
		var expectedList = ExpectedSiteWinLoss();
		AssertExtension.AreEqual(expectedList, actualResults);
	}

	[Fact]
	public async Task GetCompanyWinLoss_Always_ReturnsExpectedList()
	{
		// Arrange
		var request = new SuperWinLossRequest();
		var fakeData = FakeCompanyWinLoss;
		repository.FetchAsync<WinLossModel>(Arg.Any<CompanyWinLossCriteria>()).Returns(fakeData);

		// Act
		var actualResults = await service.SuperWinLoss(request);

		// Assert
		var expectedList = ExpectedCompanyWinLoss();
		AssertExtension.AreEqual(expectedList, actualResults);
	}

	[Fact]
	public async Task GetAgentWinLoss_Always_ReturnsExpectedList()
	{
		// Arrange
		var request = new AgentWinLossRequest();
		var fakeData = new ValueTuple<IEnumerable<WinLossModel>, IEnumerable<int>>
		{
			Item1 = FakeAgentWinLoss,
			Item2 = new List<int> { 3 }
		};

		repository.FetchMultipleAsync<WinLossModel, int>(Arg.Any<AgentWinLossCriteria>()).Returns(fakeData);

		// Act
		var actualResults = await service.AgentWinLossByMaster(request);

		// Assert
		var expectedList = ExpectedAgentWinLoss();
		AssertExtension.AreEqual(expectedList, actualResults);
	}
}