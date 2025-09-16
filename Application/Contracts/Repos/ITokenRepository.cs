using Domain.Entity.Authentication;

namespace Application.Contracts.Repos
{
    public interface ITokenRepository
    {
        Task CreateRefreshTokenAsync(int userId, string token, DateTime expirationDate);

        Task UpdateRefreshTokenAsync(RefreshToken refreshToken, int userId, string token, DateTime expirationDate);

        Task<RefreshToken> GetRefreshTokenByTokenAsync(string refreshToken);
        Task<RefreshToken> GetRefreshTokenByUserAsync(int userId);
    }
}
