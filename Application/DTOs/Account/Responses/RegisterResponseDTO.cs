namespace Application.DTOs.Account.Responses
{
    public class RegisterResponseDTO
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public RegisterResponseDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
