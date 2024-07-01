namespace ParlayRMS.WinLossRiskControl.Shared;

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

            return RoleId switch
            {
                4 => "master-win-loss.xlsx",
                3 => "agent-win-loss.xlsx",
                2 => "member-win-loss.xlsx",
                _ => "site-win-loss.xlsx"
            };
        }
    }

    public string SheetName { get; set; } = "Result";

    public int StartRowIndex { get; set; } = 3;

    public string Title { get; set; }

    public int RoleId { get; set; }

    public string Company { get; set; }

    public bool IsMerged { get; set; }
}