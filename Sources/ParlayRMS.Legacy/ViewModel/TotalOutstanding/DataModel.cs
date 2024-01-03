namespace ParlayRMS.Legacy.ViewModel.TotalOutstanding;

using System;

public class DataModel
{
    public DateTime Date { get; set; }

    public string Combo { get; set; }

    public string ParlaysType { get; set; }

    public string SportsType { get; set; }

        
    public string Username { get; set; }

    public int SearchBy { get; set; }

    public bool IsCornerParlay { get; set; }
}