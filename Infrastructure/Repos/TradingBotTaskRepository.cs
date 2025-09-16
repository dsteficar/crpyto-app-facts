using Application.Contracts.Repos;
using Domain.Entity.Trading.Bots;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    class TradingBotTaskRepository : GenericRepository<TradingBotTask>, ITradeBotTaskRepository
    {
        private readonly AppDbContext _context;

        public TradingBotTaskRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TradingBotTask?> GetByIdEagerAsync(int id)
        {

            var userTradeBot = await _context.TradingBotTask.Where(tbt => tbt.Id == id).Include(tbt => tbt.Settings).FirstOrDefaultAsync(); //&& pc.Timestamp1 >= startTimestamp

            return userTradeBot;
        }

        public async Task<IEnumerable<TradingBotTask>> GetPagedByUserId(int userId, int skip, int top)
        {

            var userTradeBots = await _context.TradingBotTask.Where(tbt => tbt.UserId == userId).Skip(skip).Take(top).ToListAsync(); //&& pc.Timestamp1 >= startTimestamp

            return userTradeBots;
        }

        public async Task<IEnumerable<TradingBotTask>> GetEnabledAsync()
        {
            var userTradeBots = await _context.TradingBotTask.Where(tbt => tbt.IsEnabled == true).ToListAsync(); //&& pc.Timestamp1 >= startTimestamp
            return userTradeBots;
        }

        public async Task<int> GetCountByUserIdAsync(int userId)
        {
            var userTradeBotsCount = await _context.TradingBotTask.Where(tbt => tbt.UserId == userId).CountAsync(); //&& pc.Timestamp1 >= startTimestamp
            return userTradeBotsCount;
        }
    }
}
