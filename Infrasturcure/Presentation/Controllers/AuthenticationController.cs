using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public class AuthenticationController(IServiceManger _serviceManger) : ApiBaseController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> LoginAsync(LoginDto loginDto)
        {
            var Result = await _serviceManger.AuthenticationService.LoginAsync(loginDto);
            return Ok (Result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> RegisterAsync(RegisterDto registerDto)
        {
            var Result = await _serviceManger.AuthenticationService.RegisterAsync(registerDto);
            return Ok(Result);
        }
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmailAsync(string email)
        {
            var Result = await _serviceManger.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _serviceManger.AuthenticationService.GetCurrentUserAsync(Email!);
            return Ok(Result);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddressAsync()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _serviceManger.AuthenticationService.GetCurrentUserAddressAsync(Email!);
            return Ok(Result);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddressAsync(AddressDto addressDto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _serviceManger.AuthenticationService.UpdateCurrentUserAddressAsync(addressDto,Email!);
            return Ok(Result);
        }
    }
}
