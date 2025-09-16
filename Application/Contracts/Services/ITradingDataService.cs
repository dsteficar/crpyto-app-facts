using Application.DTOs.Response;

namespace Application.Contracts.Services
{
    public interface ITradingDataService
    {
        Task<ServiceResult<IEnumerable<string>>> GetAllFuturesPairsAsync();
    }
}
