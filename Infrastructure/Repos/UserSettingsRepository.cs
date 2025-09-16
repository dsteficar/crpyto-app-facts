using Application.Contracts.Repos;
using Domain.Entity.Users;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
    {
        private readonly AppDbContext _context;

        public UserSettingsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserSettings> GetByUserIdAsync(int id)
        {

            var userSettings = await _context.UserSettings.Where(us => us.UserId == id).FirstOrDefaultAsync();

            return userSettings;
        }
    }
}
