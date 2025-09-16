using Application.Contracts.Services;
using Application.DTOs.Admin.Account.Requests;
using Application.DTOs.Admin.Account.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAdminUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginAdminRequestDTO request)
        {
            var loginResult = await _accountService.LoginWithCookieAsync(request.EmailAddress, request.Password);

            if (!loginResult.IsSuccess) return NotFound();

            var user = loginResult.Value;
            var response = _mapper.Map<LoginAdminResponseDTO>(user);

            response.Role = "Administrator";

            return Ok(response);
        }


        //[HttpPost("register")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RegisterAsync(RegisterRequestDTO request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid model.");
        //    }

        //    var result = await _accountService.RegisterAsync(request.EmailAddress, request.Password, request.Name);

        //    if (result.IsSuccess)
        //    {
        //        var response = result.Value;
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        var response = result.ErrorMessage;
        //        return BadRequest(response);
        //    }
        //}

        //[HttpPost("refresh-token")]
        //[AllowAnonymous]
        //public async Task<IActionResult> RefreshTokenAsync(GetRefreshTokenRequestDTO request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid model.");
        //    }

        //    var result = await _tokenService.ResetTokensAsync(request.RefreshToken);

        //    if (result.IsSuccess)
        //    {
        //        var response = result.Value;
        //        return Ok(response);
        //    }
        //    else
        //    {
        //        var response = result.ErrorMessage;
        //        return BadRequest(response);
        //    }
        //}

        //[HttpPost("token-login")]
        //[Authorize]
        //public IActionResult LoginWithAccessTokenAsync()
        //{
        //    return Ok("User is authenticated.");
        //}
    }
}
