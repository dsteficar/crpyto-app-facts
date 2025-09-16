using Application.DTOs.Response;
using Domain.Entity.Authentication;

namespace Application.Contracts.Services
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<ApplicationUser>>> GetAllAsync();

        Task<ServiceResult<ApplicationUser>> GetByIdAsync(int id);

        Task<ServiceResult<ApplicationUser>> GetByEmailAsync(string email);

        Task<ServiceResult<bool>> AddAsync(ApplicationUser user, string password);

        Task<ServiceResult<bool>> UpdateAsync(ApplicationUser user, string password);

        Task<ServiceResult<bool>> DeleteAsync(ApplicationUser user);
    }
}
