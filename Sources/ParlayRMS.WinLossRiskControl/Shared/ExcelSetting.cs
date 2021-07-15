using System;
using System.IO;

namespace ParlayRMS.WinLossRiskControl.Shared
{
    public class ExcelSetting
    {
        public string FileName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Company))
                {
                    return "super-win-loss.xlsx";
                }
                else if (RoleId == 4)
                {
                    return "master-win-loss.xlsx";
                }
                else if (RoleId == 3)
                {
                    return "agent-win-loss.xlsx";
                }
                else if (RoleId == 2)
                {
                    return "member-win-loss.xlsx";
                }
                else
                {
                    return "site-win-loss.xlsx";
                }
            }
        }

        public string SheetName { get; set; } = "Result";

        public int StartRowIndex { get; set; } = 3;

        public string Title { get; set; }

        public int RoleId { get; set; }

        public string Company { get; set; }

        public bool IsMerged { get; set; } = false;
    }
}