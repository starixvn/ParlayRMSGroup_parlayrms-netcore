using ParlayRMS.WinLossRiskControl.Models;

namespace ParlayRMS.WinLossRiskControl.UnitTests;

public static class DataHelper
{
    public static IEnumerable<CompanyModel> GetCompany()
    {
        return new List<CompanyModel>
        {
            new()
            {
                Site = "222223",
                Checked = true,
                IsDeposit = false
            },
            new()
            {
                Site = "222227",
                Checked = true,
                IsDeposit = false
            }
        };
    }

    public static WinLossModel MBCGroup
    {
        get
        {
            var credit = CreateWinLoss(1, 3, 4, 57);
            credit.IsDeposit = false;
            return credit;
        }
    }

    public static WinLossModel LicenseeGroup
    {
        get
        {
            var deposit = CreateWinLoss(3, 7, 10, 70);
            deposit.IsDeposit = true;
            return deposit;
        }
    }

    public static IEnumerable<WinLossModel> SiteWinLoss =>
        new List<WinLossModel> {
            MBCGroup,
            LicenseeGroup
        };

    public static WinLossModel CreateWinLoss(decimal commission, decimal realInCome, decimal realTurnover)
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
            RealTurnover = realTurnover
        };
    }

    public static WinLossModel CreateWinLoss(decimal commission, decimal realInCome, decimal realTurnover, decimal margin)
    {
        var model = CreateWinLoss(commission, realInCome, realTurnover);
        model.Margin = margin;

        return model;
    }

    public static WinLossModel ExpectedAgentWinLoss()
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
            Rows = AgentWinLoss,
            TotalRecords = 3
        };
    }

    public static List<WinLossModel> AgentWinLoss =>
    [
        CreateWinLoss(1, 3, 4, 57),
        CreateWinLoss(2, 7, 8, Convert.ToDecimal(87.5)),
        CreateWinLoss(3, 7, 10, 70)
    ];

    public static WinLossModel ExpectedCompanyWinLoss()
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
            Rows = CompanyWinLoss
        };
    }

    public static List<WinLossModel> CompanyWinLoss =>
    [
        CreateWinLoss(1, 3, 4, 57),
        CreateWinLoss(2, 7, 8, Convert.ToDecimal(87.5)),
        CreateWinLoss(3, 7, 10, 70),
        CreateWinLoss(4, 0, 0, 0)
    ];

    public static WinLossModel ExpectedSiteWinLoss()
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
            Rows = [MBCGroup]
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
            Rows = [LicenseeGroup]
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
            Rows = [credit, deposit]
        };
    }
}