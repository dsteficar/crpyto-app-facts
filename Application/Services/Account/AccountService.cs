using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Authentication;
using Domain.ResultObjects.Roles;
using Domain.ValueObjects.Tokens;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IRefreshTokenService _tokenService;
        private readonly IUserSettingsService _userSettingsService;

        public AccountService(
          IAccountRepository accountRepository,
          IRefreshTokenService tokenService,
          IUserSettingsService userSettingsService)
        {
            _accountRepository = accountRepository;
            _tokenService = tokenService;
            _userSettingsService = userSettingsService;
        }

        #region LoginAndRegistration
        public async Task<ServiceResult<UserTokenResult>> LoginWithJwtAsync(string emailAddress, string password)
        {
            try
            {
                var user = await _accountRepository.FindByEmailAsync(emailAddress);
                if (user is null)
                    return ServiceResult<UserTokenResult>.Failure($"User with email {emailAddress} was not found!");

                SignInResult result;

                try
                {
                    result = await _accountRepository.CheckLoginDataAsync(user, password, false);
                }
                catch (Exception ex)
                {
                    return ServiceResult<UserTokenResult>.Failure("Error occured with identity server: " + ex.Message);
                }

                if (!result.Succeeded)
                    return ServiceResult<UserTokenResult>.Failure("Invalid credentials");

                string jwtToken = await _tokenService.GenerateToken(user);
                string refreshToken = _tokenService.GenerateRefreshToken();

                if (string.IsNullOrEmpty(jwtToken) || string.IsNullOrEmpty(refreshToken))
                {
                    return ServiceResult<UserTokenResult>.Failure("Error occured while logging in account, please contact administration");
                }
                else
                {
                    var saveResult = await _tokenService.SaveRefreshToken(user.Id, refreshToken, DateTime.UtcNow.AddDays(7));
                    if (saveResult.IsSuccess)
                    {
                        return ServiceResult<UserTokenResult>.Success(
                            new UserTokenResult(jwtToken, refreshToken, user), "User has successfully logged in");
                    }
                    else
                    {
                        return ServiceResult<UserTokenResult>.Failure("Error occured while logging in account, please contact administration");
                    }
                }

            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw; // Let the InfrastructureException propagate up the stack
            }
        }

        public async Task<ServiceResult<ApplicationUser>> LoginWithCookieAsync(string emailAddress, string password)
        {
            try
            {
                var user = await _accountRepository.FindByEmailAsync(emailAddress);
                if (user is null)
                    return ServiceResult<ApplicationUser>.Failure($"User with email {emailAddress} was not found!");

                SignInResult result;

                try
                {
                    result = await _accountRepository.CheckLoginDataAsync(user, password, false);
                }
                catch (Exception ex)
                {
                    return ServiceResult<ApplicationUser>.Failure("Error occured with identity server: " + ex.Message);
                }

                if (!result.Succeeded)
                    return ServiceResult<ApplicationUser>.Failure("Invalid credentials");

                return ServiceResult<ApplicationUser>.Success(user, "User has successfully logged in");

            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw;
            }
        }

        public async Task<ServiceResult<string>> RegisterAsync(string emailAddress, string password, string name)
        {
            try
            {
                if (await _accountRepository.FindByEmailAsync(emailAddress) != null)
                    return ServiceResult<string>.Failure($"User with email: {emailAddress} already exists.");

                var user = new ApplicationUser()
                {
                    Name = name,
                    UserName = emailAddress,
                    Email = emailAddress,
                    PasswordHash = password
                };

                var result = await _accountRepository.AddAsync(user, password);

                string error = CheckIdentityResponse(result);

                if (!string.IsNullOrEmpty(error))
                    return ServiceResult<string>.Failure("Error occured with identity server: " + error);

                var assignUserResponse = await UpdateUserRoleAsync(emailAddress, "Admin"); // Alter this to go through real roles
                await _userSettingsService.SeedDefaultSettingsAsync(user.Id);

                return assignUserResponse;

            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw; // Let the InfrastructureException propagate up the stack
            }
        }

        #endregion

        #region IdentityManager

        public async Task<ServiceResult<string>> AddRoleAsync(string roleName)
        {
            try
            {
                if (await _accountRepository.FindRoleByNameAsync(roleName) == null)
                {
                    var response = await _accountRepository.AddUserRoleAsync(roleName);
                    var error = CheckIdentityResponse(response);
                    if (!string.IsNullOrEmpty(error))
                        return ServiceResult<string>.Failure(error);
                    else
                        return ServiceResult<string>.Success($"{roleName} role created successfully");
                }
                return ServiceResult<string>.Failure($"{roleName} already created");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw; // Let the InfrastructureException propagate up the stack
            }
        }

        public async Task<ServiceResult<string>> UpdateUserRoleAsync(string emailAddress, string roleName)
        {
            try
            {
                if (await _accountRepository.FindRoleByNameAsync(roleName) is null) return ServiceResult<string>.Failure($"{roleName} role was not found.");
                if (await _accountRepository.FindByEmailAsync(emailAddress) is null) return ServiceResult<string>.Failure($"User with email: {emailAddress} was not found.");

                var user = await _accountRepository.FindByEmailAsync(emailAddress);
                var previousUserRoles = await _accountRepository.GetUserRolesAsync(user);
                var previousRole = previousUserRoles.FirstOrDefault();

                if (previousRole != null)
                {
                    var oldRoleRemovedResult = await _accountRepository.RemoveUserFromRoleAsync(user, previousRole);

                    var oldRoleRemoveResultError = CheckIdentityResponse(oldRoleRemovedResult);

                    if (!string.IsNullOrEmpty(oldRoleRemoveResultError))
                        return ServiceResult<string>.Failure("Error occured with identity server: " + oldRoleRemoveResultError);
                }

                var addUserToRoleResult = await _accountRepository.AddUserToRoleAsync(user, roleName);

                var newRoleAddResultError = CheckIdentityResponse(addUserToRoleResult);

                if (!string.IsNullOrEmpty(newRoleAddResultError))
                    return ServiceResult<string>.Failure(newRoleAddResultError);
                else
                    return ServiceResult<string>.Success("Role changed");
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw; // Let the InfrastructureException propagate up the stack
            }
        }

        public async Task<ServiceResult<IEnumerable<UserRoleResult>>> GetAllUsersAndRolesAsync()
        {
            try
            {
                var allUsers = await _accountRepository.GetAllAsync();
                if (allUsers is null) return ServiceResult<IEnumerable<UserRoleResult>>.Failure("No users were found.");

                var list = new List<UserRoleResult>();

                foreach (var user in allUsers)
                {
                    var userRoles = await _accountRepository.GetUserRolesAsync(user);
                    var userFirstRoleName = userRoles.FirstOrDefault();
                    var roleEntity = await _accountRepository.FindRoleByNameAsync(userFirstRoleName);

                    list.Add(new UserRoleResult(user.Name, user.Email, roleEntity.Name, roleEntity.Id));
                }

                return ServiceResult<IEnumerable<UserRoleResult>>.Success(list);
            }
            catch (Exception ex) when (ex is not ApplicationException)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (ApplicationException)
            {
                throw; // Let the InfrastructureException propagate up the stack
            }
        }

        private static string CheckIdentityResponse(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var erros = result.Errors.Select(x => x.Description);
                return string.Join(Environment.NewLine, erros);
            }
            return null!;
        }

        #endregion
    }
}
