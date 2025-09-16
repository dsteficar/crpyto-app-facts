using Application.Contracts.Services;
using Application.DTOs.Account.Requests;
using Application.DTOs.Account.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IRefreshTokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IRefreshTokenService tokenService, IMapper mapper)
        {
            _accountService = accountService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDTO>> LoginAsync([FromBody] LoginRequestDTO request)
        {
            //if (!ModelState.IsValid) ILLEGAL!!!! Use validator(fluent or other)
            //{
            //    var response = "Invalid model.";
            //    return BadRequest(response);
            //}

            var loginResult = await _accountService.LoginWithJwtAsync(request.EmailAddress, request.Password);

            if(!loginResult.IsSuccess) return NotFound();

            var userToken = loginResult.Value;
            var response = _mapper.Map<LoginResponseDTO>(userToken);

            return Ok(response);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponseDTO>> RegisterAsync([FromBody] RegisterRequestDTO request)
        {
            var registerResult = await _accountService.RegisterAsync(request.Name, request.EmailAddress, request.EmailAddress);

            if (!registerResult.IsSuccess) return Conflict();

            var loginResult = await _accountService.LoginWithJwtAsync(request.EmailAddress, request.Password);

            if (!loginResult.IsSuccess) return NotFound();

            var userToken = loginResult.Value;

            var response = _mapper.Map<RegisterResponseDTO>(userToken);

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<ActionResult<GetRefreshTokenResponseDTO>> RefreshTokenAsync([FromBody] GetRefreshTokenRequestDTO request)
        {
            var resetTokensResult = await _tokenService.ResetTokensAsync(request.RefreshToken);

            if (!resetTokensResult.IsSuccess) return Unauthorized();

            var userToken = resetTokensResult.Value;
            var response = _mapper.Map<GetRefreshTokenResponseDTO>(userToken);

            return Ok(response);
        }

        [HttpPost("roles/add")]
        [Authorize]
        public async Task<ActionResult<string>> CreateRoleAsync([FromBody] AddRoleRequestDTO request)
        {
            var addRoleResult = await _accountService.AddRoleAsync(request.Name);

            if (!addRoleResult.IsSuccess) return NotFound();

            var response = addRoleResult.Value;

            return Ok(response);
        }


        [HttpPost("roles/users/update")]
        [Authorize]
        public async Task<ActionResult<string>> ChangeUserRoleAsync([FromBody] UpdateUserRoleRequestDTO request)
        {

            var updateRoleResult = await _accountService.UpdateUserRoleAsync(request.EmailAddress, request.RoleName);

            if (!updateRoleResult.IsSuccess) return NotFound();

            var response = updateRoleResult.Value;

            return Ok(response);
        }

        [HttpGet("roles/users/all")]
        [Authorize]
        public async Task<IActionResult> GetUsersWithRolesAsync()
        {
            var getUserRolesResult = await _accountService.GetAllUsersAndRolesAsync();

            if (!getUserRolesResult.IsSuccess) return NotFound();
            
            var response = _mapper.Map<List<GetUserRoleResponseDTO>>(getUserRolesResult.Value);

            return Ok(response);
        }
    }
}

