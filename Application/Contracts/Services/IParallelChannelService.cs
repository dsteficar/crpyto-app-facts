using Application.DTOs.Response;
using Domain.Entity.Trading.Graphs;

namespace Application.Contracts.Services
{
    public interface IParallelChannelService : IGenericService<ParallelChannel>
    {
        //Task<Result<ParallelChannel>> AddChannelForUserAsync(int userId, decimal price1, decimal timestamp1, decimal price2, decimal timestamp2, decimal price3, decimal timestamp3);

        Task<ServiceResult<List<ParallelChannel>>> GetUserParallelChannels(int userId, decimal startTimestamp);
    }
}
