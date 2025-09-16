using Domain.Entity.Trading.Graphs;

namespace Application.Contracts.Repos
{
    public interface IParallelChannelRepository : IGenericRepository<ParallelChannel>
    {
        Task<List<ParallelChannel>> GetUserParallelChannels(int userId, decimal startTimestamp);

        //Task<ParallelChannel> AddChannelForUserAsync(ParallelChannel parallelChannel);
    }
}
