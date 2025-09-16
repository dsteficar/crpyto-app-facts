using Application.DTOs.Response;

namespace Application.Contracts.Services
{
    public interface IBinanceService
    {
        Task<ServiceResult<IEnumerable<string>>> GetAllFuturesPairsAsync();
    }
}
