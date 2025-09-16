using Application.Contracts.Repos;
using Application.Contracts.Services;
using Application.DTOs.Response;
using Domain.Entity.Authentication;
using Domain.ValueObjects.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.Tokens
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IConfiguration _config;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAccountRepository _accountRepository;

        public RefreshTokenService(IConfiguration config, ITokenRepository tokenRepository, IAccountRepository accountRepository)
        {
            _config = config;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
        }

        public string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)); //Convert to GUID?

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var userClaims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, "Trader"),
                    new Claim("Fullname", user.Name)
                };
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: userClaims,
                    expires: DateTime.UtcNow.AddMinutes(15),
                    signingCredentials: credentials
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<ServiceResult<UserTokenResult>> ResetTokensAsync(string refreshToken)
        {
            try
            {
                var token = await _tokenRepository.GetRefreshTokenByTokenAsync(refreshToken);
                if (token is null) return ServiceResult<UserTokenResult>.Failure("Refresh token has not been found.");

                if (token.ExpirationDate < DateTime.UtcNow)
                    return ServiceResult<UserTokenResult>.Failure("Refresh token has expired.");

                var user = await _accountRepository.FindByIdAsync((int)token.UserId);
                string newToken = await GenerateToken(user!);
                string newRefreshToken = GenerateRefreshToken();

                var saveResult = await SaveRefreshToken(user!.Id, newRefreshToken, DateTime.UtcNow.AddDays(7));

                if (saveResult.IsSuccess)
                    return ServiceResult<UserTokenResult>.Success(new UserTokenResult(newToken, newRefreshToken, user), "User tokens were successfully rest.");
                else
                    return ServiceResult<UserTokenResult>.Failure("Error has occured while saving the new refresh token.");
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

        public async Task<ServiceResult<string>> SaveRefreshToken(int userId, string token, DateTime expirationDate)
        {
            try
            {
                var userRefreshToken = await _tokenRepository.GetRefreshTokenByUserAsync(userId);

                if (userRefreshToken is null)
                {
                    await _tokenRepository.CreateRefreshTokenAsync(userId, token, expirationDate);
                }
                else
                {
                    await _tokenRepository.UpdateRefreshTokenAsync(userRefreshToken, userId, token, expirationDate);
                }

                return ServiceResult<string>.Success("Tokens were successfully saved.");
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
