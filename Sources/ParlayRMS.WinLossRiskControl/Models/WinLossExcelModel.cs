using System.Collections.Generic;
using Epplus.Mapper.Annotations;

namespace ParlayRMS.WinLossRiskControl.Models
{
    public class WinLossExcelModel
    {
        private string username;

        [Cell("A3")]
        public string Username {
             get{
                if(string.IsNullOrWhiteSpace(username))
                { 
                    switch(IsDeposit)
                    {
                        case true:
                            return "Licensee Group";
                        case false:
                            return "MBC Group";
                        default:
                            return "Grand Total";
                    }
                }
                else {
                    return username;
                }
            }
            set {
                username = value;
            }
        }

        public bool? IsDeposit { get; set; }

        [Cell("B3")]
        public int BetCount { get; set; }

        [Cell("C3")]
        public decimal MemberWinLoss { get; set; }

        [Cell("D3")]
        public decimal SmaWinLoss { get; set; }

        [Cell("E3")]
        public decimal ExtraPT { get; set; }

        [Cell("F3")]
        public decimal Commission { get; set;}

        [Cell("G3")]
        public decimal RealIncome { get; set;}

        [Cell("H3")]
        public decimal RealTurnover { get; set;}

        [Cell("I3")]
        public decimal Margin { get; set;}

        public List<WinLossModel> Rows { get; set; }  =  new List<WinLossModel>();
        
        public void SumTotal(WinLossModel site)
        {
            this.BetCount += site.BetCount;
            this.MemberWinLoss += site.MemberWinLoss;
            this.SmaWinLoss += site.SmaWinLoss;
            this.ExtraPT += site.ExtraPT;
            this.Commission += site.Commission;
            this.RealIncome += site.RealIncome;
            this.RealTurnover += site.RealTurnover;
            this.Margin = this.RealTurnover > 0 ? (this.RealIncome/this.RealTurnover) * 100 : 0;
        }
    }
}