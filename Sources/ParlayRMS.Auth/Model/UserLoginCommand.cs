namespace ParlayRMS.Auth.Model;

using Fanex.Data.Repository;

public class UserLoginCommand : INonQueryCommand
{
	public string Username { get; set; }

	public string Userpwd { get; set; }

	public string LoginIp { get; set; }

	public string LoginDomain { get; set; }

	public string BrowserName { get; set; }

	public string Platform { get; set; }

	public string CountryCode { get; set; }

	public string Language { get; set; }

	public int CustId { get; set; }

	public string GetSettingKey() => "RMS_UserLogin";

	public bool IsValid() => true;
}