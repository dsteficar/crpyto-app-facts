using Domain.Entity.Users;

namespace Application.Contracts.Repos
{
    public interface IUserSettingsRepository : IGenericRepository<UserSettings>
    {
        Task<UserSettings> GetByUserIdAsync(int id);
    }
}
