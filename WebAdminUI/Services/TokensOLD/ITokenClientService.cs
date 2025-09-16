namespace WebAdminUI.Services.Tokens
{
    public interface ITokenClientService
    {
        Task<string?> GetAccessTokenAsync();
        Task<string?> GetRefreshTokenAsync();
        Task SetAccessTokenAsync(string token);
        Task SetRefreshTokenAsync(string token);
        Task RefreshTokensAsync();
    }
}
