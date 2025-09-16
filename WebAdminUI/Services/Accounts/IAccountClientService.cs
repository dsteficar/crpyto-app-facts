using Application.DTOs.Admin.Account.Requests;
using Application.DTOs.Admin.Account.Responses;

namespace WebAdminUI.Services.Accounts
{
    public interface IAccountClientService
    {
        Task<LoginAdminResponseDTO> LoginAsync(LoginAdminRequestDTO requestDto);

        Task<bool> LogoutAsync();
    }
}
