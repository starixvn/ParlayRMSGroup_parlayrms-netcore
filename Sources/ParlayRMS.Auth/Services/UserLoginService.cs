using Fanex.Data.Repository;
using Newtonsoft.Json;
using ParlayRMS.Auth.Model;
using System.Threading.Tasks;

namespace ParlayRMS.Auth.Services;

public interface UserLoginService
{
	Task<UserLoginResult> Login(string parameters);
}

public class UserLoginServiceImp : UserLoginService
{
	private readonly IDynamicRepository repository;

	public UserLoginServiceImp(IDynamicRepository repository)
	{
		this.repository = repository;
	}

	public async Task<UserLoginResult> Login(string parameters)
	{
		var command = JsonConvert.DeserializeObject<UserLoginCommand>(parameters);
		await repository.ExecuteAsync(command);

		return new UserLoginResult
		{
			UserId = command.CustId,
			Username = command.Username
		};
	}
}