using Application.Contracts.Repos;
using Domain.Entity.Trading.Bots;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    class TradingBotSettingsRepository : GenericRepository<TradingBotSettings>, ITradeBotSettingsRepository
    {
        private readonly AppDbContext _context;

        public TradingBotSettingsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TradingBotSettings> GetByTradeBotTaskIdAsync(int id)
        {
            var result = await _context.TradingBotSettings.FirstOrDefaultAsync(tbs => tbs.TradingBotTaskId == id);

            return result;
        }
    }
}
