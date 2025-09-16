using Application.Contracts.Repos;
using Domain.Entity.Authentication;
using Domain.Entity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class AccountRepository : IAccountRepository
    {

        private readonly RoleManager<ApplicationUserRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(
            RoleManager<ApplicationUserRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            return result;
        }

        public async Task<IdentityResult> AddAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            return result;
        }

        public async Task<ApplicationUser> FindByIdAsync(int id)
        {
            var result = await _userManager.FindByIdAsync(id.ToString());
            return result;
        }

        public async Task<IdentityResult> AddUserRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new ApplicationUserRole() { Name = roleName });
            return result;
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            var genericRoleList = await _userManager.GetRolesAsync(user);
            var result = genericRoleList.ToList();
            return result;
        }
        public async Task<ApplicationUserRole> FindRoleByNameAsync(string roleName)
        {
            var result = await _roleManager.FindByNameAsync(roleName);
            return result;
        }

        public async Task<IdentityResult> RemoveUserFromRoleAsync(ApplicationUser user, string roleName)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            return result;
        }

        public async Task<IdentityResult> AddUserToRoleAsync(ApplicationUser user, string roleName)
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        public async Task<SignInResult> CheckLoginDataAsync(ApplicationUser user, string password, bool lockoutOnFailer = false)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result;
        }
    }
}
