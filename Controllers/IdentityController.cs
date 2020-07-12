using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PepegaFoodServer.Contracts;
using PepegaFoodServer.Contracts.Requests;
using PepegaFoodServer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PepegaFoodServer.Controllers
{
    public class IdentityController:Controller
    {
        private readonly IIdentityService _identityService;


        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> RegistrationUser([FromBody]RegUserRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request model is not correct");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage)));
            }

            var authResponse = await _identityService.RegisterAsync(request);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.ErrorsMessages);
            }

            return Ok(authResponse);

        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request model is not correct");
            }

            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse.ErrorsMessages);
            }

            return Ok(authResponse);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(ApiRoutes.Identity.CheckJWT)]
        public IActionResult CheckJWT()
        {
            var userNameFromJwt = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserName").Value.ToString();

            var response = new { tokenActive = true, userName = userNameFromJwt };

            return Ok(response);
        }

    }

}

