namespace ParlayRMS.ManageAlert.Excels;

public class ExcelSetting
{
    public string? FileName { get; set; }

    public int StartRowIndex => 2;

    public string SheetName { get; set; }
}