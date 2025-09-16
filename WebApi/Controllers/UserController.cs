using Application.Contracts.Repos;
using Application.DTOs.Response.User;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase //In work, maybe migrate roles to this part instead of in account(keep it tied to a specific account)
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsersAsync()
        {
            return Ok(await _userRepository.GetAllUsersAsync());
        }
    }
}
