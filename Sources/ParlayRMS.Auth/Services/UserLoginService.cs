using Fanex.Data.Repository;
using Newtonsoft.Json;
using ParlayRMS.Auth.Criteria;
using System.Threading.Tasks;

namespace ParlayRMS.Auth.Services
{
    public interface UserLoginService
    {
        Task<bool> Login(string parameters);
    }

    public class UserLoginServiceImp : UserLoginService
    {
        private readonly IDynamicRepository repository;

        public UserLoginServiceImp(IDynamicRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> Login(string parameters)
        {
            var command = JsonConvert.DeserializeObject<UserLoginCommand>(parameters);
            await repository.ExecuteAsync(command);
            return command.CustId > 0;
        }
    }
}