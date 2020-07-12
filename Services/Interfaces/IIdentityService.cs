using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PepegaFoodServer.Services.Interfaces
{
    public interface IIdentityService
    {
        public Task<ChangedInformationResultModel> RegisterAsync(RegUserRequest userInfo);

        public Task<AuthResultModel> LoginAsync(string email, string password);

        public Task<List<ResponseUserModel>> GetAllUsersInSystemAsync();

    }
}
