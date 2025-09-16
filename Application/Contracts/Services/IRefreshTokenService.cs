using Application.DTOs.Response;
using Domain.Entity.Authentication;
using Domain.ValueObjects.Tokens;

namespace Application.Contracts.Services
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();

        Task<string> GenerateToken(ApplicationUser user);

        Task<ServiceResult<UserTokenResult>> ResetTokensAsync(string refreshToken);

        Task<ServiceResult<string>> SaveRefreshToken(int userId, string token, DateTime expirationDate);

    }
}
