﻿namespace Application.DTOs.Account.Responses
{
    public class LoginResponseDTO
    {
        public string AccessToken { get; set; }
     
        public string RefreshToken { get; set; }

        public LoginResponseDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

    }
}
