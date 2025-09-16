using Application.Contracts.Repos;
using Domain.Entity.Authentication;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class RefreshTokenRepository : ITokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateRefreshTokenAsync(int userId, string token, DateTime expirationDate)
        {
            _context.RefreshTokens.Add(new RefreshToken() { UserId = userId, Token = token, ExpirationDate = expirationDate });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken, int userId, string token, DateTime expirationDate)
        {
            refreshToken.UserId = userId;
            refreshToken.Token = token;
            refreshToken.ExpirationDate = expirationDate;

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenByTokenAsync(string refreshToken)
        {
            var userRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
            return userRefreshToken;
        }

        public async Task<RefreshToken> GetRefreshTokenByUserAsync(int userId)
        {
           var userRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == userId);
            return userRefreshToken;
        }
    }
}
