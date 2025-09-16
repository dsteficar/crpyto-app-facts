using Domain.Entity.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Application.Contracts.Repos
{
    public interface IAccountRepository
    {
        Task<List<ApplicationUser>> GetAllAsync();

        Task<IdentityResult> AddAsync(ApplicationUser user, string password);

        Task<IdentityResult> UpdateAsync(ApplicationUser user);

        Task<IdentityResult> DeleteAsync(ApplicationUser user);

        Task<ApplicationUser> FindByEmailAsync(string email);

        Task<ApplicationUser> FindByIdAsync(int id);

        Task<IdentityResult> AddUserRoleAsync(string roleName);

        Task<List<string>> GetUserRolesAsync(ApplicationUser user);

        Task<ApplicationUserRole> FindRoleByNameAsync(string roleName);

        Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, string roleName);

        Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName);

        Task<SignInResult> CheckLoginDataAsync(ApplicationUser user, string password, bool lockoutOnFailer = false);

    }
}
