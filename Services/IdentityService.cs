using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Data;
using PepegaFoodServer.Models;
using PepegaFoodServer.Models.DbModels;
using PepegaFoodServer.Options;
using PepegaFoodServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PepegaFoodServer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly DataContext _dataContext;

        public IdentityService(UserManager<UserModel> userManager, JwtSettings jwtSettings, PasswordOptions passOptions, DataContext dataContext)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
            _userManager.Options.Password = passOptions;
            _dataContext = dataContext;
        }

        public async Task<ChangedInformationResultModel> RegisterAsync(RegUserRequest userInfo)
        {
            var existingUser = await _userManager.FindByEmailAsync(userInfo.Email);

            if (existingUser != null)
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = new[] { "User with this email addres already exist" } };
            }

            var userName = userInfo.Email.Substring(0, userInfo.Email.LastIndexOf('@'));

            var newUser = new UserModel
            {
                Email = userInfo.Email,
                UserName = userName,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Address = userInfo.Address,
                PhoneNumber = userInfo.PhoneNumber,
            };

            var createdUser = await _userManager.CreateAsync(newUser, userInfo.Password);

            if (!createdUser.Succeeded)
            {
                return new ChangedInformationResultModel { Success = false, ErrorsMessages = createdUser.Errors.Select(x => x.Description) };
            }

            return new ChangedInformationResultModel { Success = true };

        }

        public async Task<AuthResultModel> LoginAsync(string email, string password)
        {

            var existingUser = await _dataContext.Users.SingleOrDefaultAsync(x => x.Email == email);

            if (existingUser != null && await _userManager.CheckPasswordAsync(existingUser, password))
            {
                return GenerateAuthResultForUser(existingUser);
            }
            else
            {
                return new AuthResultModel { Success = false, ErrorsMessages = new[] { "Email/password incorrect" } };
            }

        }

        public async Task<List<ResponseUserModel>> GetAllUsersInSystemAsync()
        {
            var result = _userManager.Users.Select(x => new ResponseUserModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                UserName = x.UserName,
            });

            return await result.ToListAsync();
        }

        private AuthResultModel GenerateAuthResultForUser(UserModel userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("UserName", userModel.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
                        new Claim("PhoneNumber", userModel.PhoneNumber),
                        new Claim("id", userModel.Id),
                    }),
                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResultModel
            {
                Token = tokenHandler.WriteToken(token),
                Success = true,
                UserName = userModel.UserName,
            };

        }

    }
}
