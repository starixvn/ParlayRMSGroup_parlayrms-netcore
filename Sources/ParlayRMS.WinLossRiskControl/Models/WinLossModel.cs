using System.Collections.Generic;

namespace ParlayRMS.WinLossRiskControl.Models
{
    public class WinLossModel
    {
        private string username;

        private decimal margin;

        public string Username
        {
            get
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    switch (IsDeposit)
                    {
                        case true:
                            return "Licensee Group";

                        case false:
                            return "MBC Group";

                        default:
                            return "Grand Total";
                    }
                }
                else
                {
                    return username;
                }
            }
            set
            {
                username = value;
            }
        }

        public int CustId { get; set; }

        public bool? IsDeposit { get; set; }

        public int RoleId { get; set; }

        public int NoMember { get; set; }

        public int BetCount { get; set; }

        public decimal MemberWinLoss { get; set; }

        public decimal SmaWinLoss { get; set; }

        public decimal ExtraPT { get; set; }

        public decimal Commission { get; set; }

        public decimal RealIncome { get; set; }

        public decimal RealTurnover { get; set; }

        public string Site { get; set; }

        public int TotalRecords { get; set; }

        public decimal Margin
        {
            get
            {
                return this.RealTurnover > 0 ? (this.RealIncome / this.RealTurnover) * 100 : 0;
            }
            set
            {
                margin = value;
            }
        }

        public List<WinLossModel> Rows { get; set; } = new List<WinLossModel>();

        public void SumTotal(WinLossModel site)
        {
            this.NoMember += site.NoMember;
            this.BetCount += site.BetCount;
            this.MemberWinLoss += site.MemberWinLoss;
            this.SmaWinLoss += site.SmaWinLoss;
            this.ExtraPT += site.ExtraPT;
            this.Commission += site.Commission;
            this.RealIncome += site.RealIncome;
            this.RealTurnover += site.RealTurnover;
            this.Margin = this.RealTurnover > 0 ? (this.RealIncome / this.RealTurnover) * 100 : 0;
        }
    }
}