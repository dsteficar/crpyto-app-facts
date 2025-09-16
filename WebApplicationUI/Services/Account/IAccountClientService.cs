using Application.DTOs.Account.Requests;
using Application.DTOs.Account.Responses;

namespace WebAdminUI.Services.Accounts
{
    public interface IAccountClientService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);

        Task<string> LoginWithAccessTokenAsync();

        Task LogoutAsync();

        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request);
    }
}
