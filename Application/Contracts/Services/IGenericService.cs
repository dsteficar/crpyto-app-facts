using Application.DTOs.Response;

namespace Application.Contracts.Services
{
    public interface IGenericService<T> where T : class
    {
        Task<ServiceResult<IEnumerable<T>>> GetAllAsync();
        Task<ServiceResult<IEnumerable<T>>> GetPagedAsync(int skip, int top);
        Task<ServiceResult<T>> GetByIdAsync(int id);
        Task<ServiceResult<bool>> AddAsync(T entity);
        Task<ServiceResult<bool>> UpdateAsync(T entity);
        Task<ServiceResult<bool>> DeleteAsync(T entity);
        Task<ServiceResult<int>> GetCountAsync();
    }
}