using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Users;

namespace Application.Services.Users
{
    public class UserSettingsService : GenericService<UserSettings>, IUserSettingsService
    {
        private readonly IUserSettingsRepository _userSettingsRepository;
        private readonly IUserService _userService;

        public UserSettingsService(IUserSettingsRepository usetSettingsRepository, IUserService userService) : base(usetSettingsRepository)
        {
            _userSettingsRepository = usetSettingsRepository;
            _userService = userService;
        }

        public async Task<ServiceResult<UserSettings>> SeedDefaultSettingsAsync(int userId)
        {
            try
            {
                var existingSettings = await _userSettingsRepository.GetByIdAsync(userId);

                if (existingSettings == null)
                {
                    var defaultSettings = new UserSettings
                    {
                        UserId = userId,
                        Theme = "light",
                        Language = "en",
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _userSettingsRepository.AddAsync(defaultSettings);
                    await _userSettingsRepository.SaveChangesAsync();

                    if (defaultSettings.Id != 0)
                    {
                        return ServiceResult<UserSettings>.Success(defaultSettings, "Added default settings for the user");
                    }
                    else
                    {
                        return ServiceResult<UserSettings>.Failure("Error trying to seed default data");
                    }
                }

                return ServiceResult<UserSettings>.Success(existingSettings, "Retrieved existing default settings for the user");
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

        public async Task<ServiceResult<UserSettings>> GetByUserEmail(string email)
        {
            try
            {
                var getUserResult = await _userService.GetByEmailAsync(email);
                if (!getUserResult.IsSuccess) return ServiceResult<UserSettings>.Failure($"Error trying to fetch user with Email {email}. No user found.");

                var user = getUserResult.Value;

                var userSettings = await _userSettingsRepository.GetByUserIdAsync(user.Id);

                if (userSettings != null)
                {
                    return ServiceResult<UserSettings>.Success(userSettings, "User settings retrieved.");
                }
                else
                {
                    return ServiceResult<UserSettings>.Failure("User settings do not exist.");
                }
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

    }
}
