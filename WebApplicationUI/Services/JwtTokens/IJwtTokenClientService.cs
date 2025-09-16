namespace WebApplicationUI.Services.JwtTokens
{
    public interface IJwtTokenClientService
    {
        Task<string?> GetAccessTokenAsync();
        Task<string?> GetRefreshTokenAsync();
        Task SetAccessTokenAsync(string token);
        Task SetRefreshTokenAsync(string token);
        Task DeleteAccessTokenAsync();
        Task DeleteRefreshTokenAsync();
        Task<bool> RefreshTokensAsync();
    }
}
