using Application.DTOs.ParallelChannel.Requests;
using Application.DTOs.ParallelChannel.Responses;

namespace WebApplicationUI.Services.TradingViewGraph
{
    public interface ITradingViewGraphClientService
    {
        Task<IEnumerable<string>> GetAllFuturesPairSymbolsAsync(CancellationToken cancelToken);
        Task<AddParallelChannelResponseDTO> AddParallelChannelForUser(AddParallelChannelRequestDTO dto, CancellationToken cancelToken);
        Task UpdateParallelChannel(UpdateParallelChannelRequestDTO dto, CancellationToken cancelToken);
        Task DeleteParallelChannel(int parallelChannelId, CancellationToken cancelToken);
        Task<List<GetParallelChannelDTO>> FindChannelsByUserIdAndStartTimestamp(int userId, decimal startTimestamp, CancellationToken cancelToken);
    }
}
