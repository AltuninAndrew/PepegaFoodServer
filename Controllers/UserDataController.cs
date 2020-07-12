using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PepegaFoodServer.Contracts;
using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Controllers
{
    public class UserDataController : Controller
    {
        private readonly IUserDataService _userDataService;

        public UserDataController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(ApiRoutes.ClientData.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromRoute] string username, [FromBody]ChangePasswordRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request model is not correct");
            }

            var changeResponse = await _userDataService.ChangePasswordAsync(username, request.OldPassword, request.NewPassword);

            if (changeResponse.Success)
            {
                return Ok("Password change is successful");
            }
            else
            {
                return BadRequest(changeResponse.ErrorsMessages);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Identity.GetUserInfo)]
        public async Task<IActionResult> GetUserInfo([FromRoute] string username)
        {
            var userNameFromJwt = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName").Value.ToString();

            if (userNameFromJwt == username)
            {
                var response = await _userDataService.GetUserInfoAsync(username);

                if (response == null)
                {
                    return BadRequest("User not found");
                }

                return Ok(response);

            }
            else
            {
                return Forbid();
            }


        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(ApiRoutes.ClientData.ChangeEmail)]
        public async Task<IActionResult> ChangeEmail([FromRoute] string username, [FromBody] ChangeEmailRequest request)
        {
            if (request == null || (!ModelState.IsValid))
            {
                return BadRequest("Request model is not correct");
            }

            var userNameFromJwt = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName").Value.ToString();
            if (userNameFromJwt == username)
            {
                var changeResponse = await _userDataService.ChangeEmailAsync(username, request.NewEmail);

                if (!changeResponse.Success)
                {
                    return BadRequest(changeResponse.ErrorsMessages);
                }

                return Ok("Email chage is successful");
            }
            else
            {
                return Forbid();
            }


        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(ApiRoutes.ClientData.ChangeFirstName)]
        public async Task<IActionResult> ChangeFirstName([FromRoute] string username, [FromBody] ChangeFirstNameRequest request)
        {
            if (string.IsNullOrEmpty(request.NewFirstName))
            {
                return BadRequest("Request model is not correct");
            }


            if (request.NewFirstName.Length < 2)
            {
                return BadRequest("Length should be more then 1 chars");
            }

            var userNameFromJwt = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName").Value.ToString();
            if (userNameFromJwt == username)
            {
                var changeResponse = await _userDataService.ChangeFirstNameAsync(username, request.NewFirstName);

                if (!changeResponse.Success)
                {
                    return BadRequest(changeResponse.ErrorsMessages);
                }

                return Ok("First name was successfully change");

            }
            else
            {
                return Forbid();
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut(ApiRoutes.ClientData.ChangeLastName)]
        public async Task<IActionResult> ChangeLastName([FromRoute] string username, [FromBody]ChangeLastNameRequest request)
        {
            if (string.IsNullOrEmpty(request.NewLastName))
            {
                return BadRequest("Request model is not correct");
            }

            if (request.NewLastName.Length < 2)
            {
                return BadRequest("Length should be more then 1 chars");
            }

            var userNameFromJwt = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName").Value.ToString();
            if (userNameFromJwt == username)
            {
                var changeResponse = await _userDataService.ChangeLastNameAsync(username, request.NewLastName);

                if (!changeResponse.Success)
                {
                    return BadRequest(changeResponse.ErrorsMessages);
                }

                return Ok("Last name was successfully change");

            }
            else
            {
                return Forbid();
            }

        }


        [HttpDelete(ApiRoutes.ClientData.DeleteUser)]
        public async Task<IActionResult> DeleteUser([FromRoute] string username)
        {

            var changeResponse = await _userDataService.DeleteUserAsync(username);

            if (!changeResponse.Success)
            {
                return BadRequest(changeResponse.ErrorsMessages);
            }

            return Ok("User was successfully deleted");

        }

    }
}
