using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DomanLayer.Models.Identity_models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApstractionLayer;
using Shared.DTOS.IdentityDtos;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController (IServiceManager _serviceManager):ControllerBase
    {
        //Login
        [HttpPost("login")]

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _serviceManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(user);    
        }

        [HttpPost("Register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("ChekEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var res = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(res);
        }

        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var appuser = await _serviceManager.AuthenticationService.GetCurrentUser(email!);
            return Ok(appuser);
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<Address>> GetCurrentUserAdress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManager.AuthenticationService.GetUserAdress(email!);
            return Ok(address);     
        }

        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<Address>> UpdateGetCurrentUserAdress(Address address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var updateaddress = await _serviceManager.AuthenticationService.UpdateCurrentUserAddress(email!,address);
            return Ok(updateaddress);
        }
    }
}
