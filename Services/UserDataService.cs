using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PepegaFoodServer.Data;
using PepegaFoodServer.Models;
using PepegaFoodServer.Models.DbModels;
using PepegaFoodServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly UserManager<UserDBModel> _userManager;
        private readonly DataContext _dataContext;

        public UserDataService(UserManager<UserDBModel> userManager, DataContext dataContext)
        {
            _userManager = userManager;
            _dataContext = dataContext;
        }

        public async Task<ChangedInformationResultModel> ChangeAddress(string username, string newAddress)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                foundUser.Address = newAddress;
                IdentityResult identityResult = await _userManager.UpdateAsync(foundUser);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found" } };
            }
        }

        public async Task<ChangedInformationResultModel> ChangeEmailAsync(string username, string newEmail)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                if (newEmail == foundUser.Email)
                {
                    return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "New address is no different from the old one" } };
                }

                foundUser.Email = newEmail;
                foundUser.UserName = newEmail.Substring(0, newEmail.LastIndexOf('@'));
                IdentityResult identityResult = await _userManager.UpdateAsync(foundUser);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found or password incorrect" } };
            }
        }

        public async Task<ChangedInformationResultModel> ChangeFirstNameAsync(string username, string newFirstName)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                foundUser.FirstName = newFirstName;
                IdentityResult identityResult = await _userManager.UpdateAsync(foundUser);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found" } };
            }
        }

        public async Task<ChangedInformationResultModel> ChangeLastNameAsync(string username, string newLastName)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                foundUser.LastName = newLastName;
                IdentityResult identityResult = await _userManager.UpdateAsync(foundUser);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found" } };
            }
        }

        public async Task<ChangedInformationResultModel> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                IdentityResult identityResult = await _userManager.ChangePasswordAsync(foundUser, oldPassword, newPassword);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found" } };
            }

        }

        public async Task<ChangedInformationResultModel> ChangePhoneNumber(string username, string newPhoneNumber)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                foundUser.PhoneNumber = newPhoneNumber;
                IdentityResult identityResult = await _userManager.UpdateAsync(foundUser);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found" } };
            }
        }

        public async Task<ChangedInformationResultModel> DeleteUserAsync(string username)
        {
            var foundUser = await _userManager.FindByNameAsync(username);

            if (foundUser != null)
            {
                await _dataContext.SaveChangesAsync();
                IdentityResult identityResult = await _userManager.DeleteAsync(foundUser);
                return new ChangedInformationResultModel { Success = identityResult.Succeeded, ErrorsMessages = identityResult.Errors.Select(x => x.Description) };
            }
            else
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User not found" } };
            }
        }

        public async Task<ResponseUserModel> GetUserInfoAsync(string username)
        {
            var foundUser = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            if (foundUser != null)
            {
                var result = new ResponseUserModel
                {
                    Email = foundUser.Email,
                    UserName = foundUser.UserName,
                    FirstName = foundUser.FirstName,
                    LastName = foundUser.LastName,
                    Address = foundUser.Address,
                    PhoneNumber = foundUser.PhoneNumber,
                };

                return result;
            }
            else
            {
                return null;
            }

        }
    }
}
