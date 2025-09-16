using Application.Contracts.Services;
using Application.DTOs.Admin;
using AutoMapper;
using Domain.Entity.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAdminUI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IMapper _mapper;
        //private readonly IUserService _userService;

        //public UserController(
        //       IMapper mapper,
        //       IUserService userService)
        //{
        //    _userService = userService;
        //    _mapper = mapper;
        //}

        //[HttpGet("all")]
        //[Authorize]
        //public async Task<ActionResult<List<UserTableAdminDTO>>> GetAllAsync()
        //{
        //    var getResult = await _userService.GetAllAsync();

        //    if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

        //    var response = _mapper.Map<List<UserTableAdminDTO>>(getResult.Value.ToList());

        //    return Ok(response);
        //}


        //[HttpPost("add")]
        //[Authorize]
        //public async Task<ActionResult<UserTableAdminDTO>> AddParallelChannel([FromBody] UserTableAdminDTO request)
        //{
        //    var user = _mapper.Map<ApplicationUser>(request);

        //    await _userService.AddAsync(user, request.PasswordHash);

        //    var response = _mapper.Map<UserTableAdminDTO>(user);

        //    return Ok(response);
        //}

        //[HttpPost("update")]
        //[Authorize]
        //public async Task<ActionResult<UserTableAdminDTO>> UpdateParallelChannel([FromBody] UserTableAdminDTO request)
        //{
        //    var getResult = await _userService.GetByIdAsync(request.Id);

        //    if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

        //    var user = getResult.Value;

        //    _mapper.Map(request, user);

        //    await _userService.UpdateAsync(user, user.PasswordHash);

        //    return Ok(user);
        //}


        //[HttpDelete("delete/{id}")]
        //[Authorize]
        //public async Task<ActionResult> DeleteAsync(int id)
        //{
        //    var getResult = await _userService.GetByIdAsync(id);

        //    if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

        //    var user = getResult.Value;

        //    await _userService.DeleteAsync(user);

        //    return Ok("User sucessfully deleted.");
        //}
    }
}
