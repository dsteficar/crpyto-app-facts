using Application.DTOs.Response;
using Domain.Entity.Authentication;
using Domain.ResultObjects.Roles;
using Domain.ValueObjects.Tokens;

namespace Application.Contracts.Services
{
    public interface IAccountService
    {
        Task<ServiceResult<UserTokenResult>> LoginWithJwtAsync(string emailAddress, string password);

        Task<ServiceResult<ApplicationUser>> LoginWithCookieAsync(string emailAddress, string password);

        Task<ServiceResult<string>> RegisterAsync(string emailAddress, string password, string name);

        Task<ServiceResult<string>> AddRoleAsync(string roleName);

        Task<ServiceResult<string>> UpdateUserRoleAsync(string emailAddress, string roleName);

        Task<ServiceResult<IEnumerable<UserRoleResult>>> GetAllUsersAndRolesAsync();
    }
}
