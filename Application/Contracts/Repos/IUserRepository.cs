using Domain.Entity.Users;

namespace Application.Contracts.Repos
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync();
    }
}
