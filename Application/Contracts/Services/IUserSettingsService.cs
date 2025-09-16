using Application.DTOs.Response;
using Domain.Entity.Users;

namespace Application.Contracts.Services
{
    public interface IUserSettingsService : IGenericService<UserSettings>
    {
        Task<ServiceResult<UserSettings>> SeedDefaultSettingsAsync(int userId);
        Task<ServiceResult<UserSettings>> GetByUserEmail(string email);
    }
}
