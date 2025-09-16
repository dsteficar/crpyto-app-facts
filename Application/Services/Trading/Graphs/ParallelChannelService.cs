using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Trading.Graphs;

namespace Application.Services.Trading.Graphs
{
    public class ParallelChannelService : GenericService<ParallelChannel>, IParallelChannelService
    {
        private readonly IParallelChannelRepository _parallelChannelRepository;

        public ParallelChannelService(IParallelChannelRepository parallelChannelRepository) : base(parallelChannelRepository)
        {
            _parallelChannelRepository = parallelChannelRepository;
        }

        //public async Task<Result<ParallelChannel>> AddChannelForUserAsync(int userId, decimal price1, decimal timestamp1, decimal price2, decimal timestamp2, decimal price3, decimal timestamp3)
        //{
        //    try
        //    {
        //        var parallelChannel = new ParallelChannel
        //        {
        //            UserId = userId,
        //            Price1 = price1,
        //            Price2 = price2,
        //            Price3 = price3,
        //            Timestamp1 = timestamp1,
        //            Timestamp2 = timestamp2,
        //            Timestamp3 = timestamp3,
        //            CreatedDate = DateTime.Now
        //        };

        //        await _parallelChannelRepository.AddChannelForUserAsync(parallelChannel);
        //        return Result<ParallelChannel>.Success(parallelChannel,"Parallel channel was successfully saved.");
        //    }
        //    catch (Exception ex) when (ex is not InfrastructureException)
        //    {
        //        throw new InfrastructureException(ex.Message);
        //    }
        //    catch (InfrastructureException)
        //    {
        //        throw;
        //    }
        //}

        public async Task<ServiceResult<List<ParallelChannel>>> GetUserParallelChannels(int userId, decimal startTimestamp)
        {
            try
            {
                var parallelChannels = await _parallelChannelRepository.GetUserParallelChannels(userId, startTimestamp);

                if (parallelChannels.Count == 0)
                    return ServiceResult<List<ParallelChannel>>.Failure("No parallel channels found.");
                else
                    return ServiceResult<List<ParallelChannel>>.Success(parallelChannels, "Parallel channel successfully retireved.");
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
