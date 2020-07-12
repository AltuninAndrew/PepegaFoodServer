using PepegaFoodServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Services.Interfaces
{
    public interface IUserDataService
    {
        public Task<ResponseUserModel> GetUserInfoAsync(string username);

        public Task<ChangedInformationResultModel> ChangeEmailAsync(string username, string newEmail);

        public Task<ChangedInformationResultModel> ChangePasswordAsync(string username, string oldPassword, string newPassword);

        public Task<ChangedInformationResultModel> ChangeFirstNameAsync(string username, string newFirstName);

        public Task<ChangedInformationResultModel> ChangeLastNameAsync(string username, string newLasttName);

        public Task<ChangedInformationResultModel> DeleteUserAsync(string username);

        public Task<ChangedInformationResultModel> ChangePhoneNumber(string username, string newPhoneNumber);

        public Task<ChangedInformationResultModel> ChangeAddress(string username, string newAddress);
    }
}
