using Application.Contracts.Repos;
using Domain.Entity.Trading.Graphs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class ParallelChannelRepository : GenericRepository<ParallelChannel>, IParallelChannelRepository
    {
        private readonly AppDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public ParallelChannelRepository(AppDbContext context, IAccountRepository accountRepository) : base(context)
        {
            _context = context;
            _accountRepository = accountRepository;
        }

        public async Task<List<ParallelChannel>> GetUserParallelChannels(int userId, decimal startTimestamp)
        {
            var parallelChannels = await _context.ParallelChannels.Where(pc => pc.UserId == userId).ToListAsync(); //&& pc.Timestamp1 >= startTimestamp

            return parallelChannels;
        }

        //public async Task<ParallelChannel> AddChannelForUserAsync(ParallelChannel parallelChannel)
        //{
        //    var user = await _accountRepository.FindUserByIdAsync(parallelChannel.UserId);

        //    if (user != null)
        //    {
        //        await _context.ParallelChannels.AddAsync(parallelChannel);
        //        await _context.SaveChangesAsync();
        //    };

        //    return parallelChannel;
        //}
    }
}
