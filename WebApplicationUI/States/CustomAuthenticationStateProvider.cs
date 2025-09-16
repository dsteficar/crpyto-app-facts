using Application.Contracts.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Runtime.InteropServices;
using System.Security.Claims;
using WebApplicationUI.Services.JwtTokens;

namespace WebApplicationUI.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJwtTokenClientService _tokenClientService;

        private ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "Anonymous")
            }, "JwtAuth"));

        public CustomAuthenticationStateProvider(IJwtTokenClientService tokenClientService)
        {
            _tokenClientService = tokenClientService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = await ValidateAndRefreshTokenAsync();
            return new AuthenticationState(claimsPrincipal);
        }

        public static ClaimsPrincipal SetUserClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, claims.NameIdentifier),
                new Claim(ClaimTypes.Email, claims.Email),
                new Claim(ClaimTypes.Name, claims.Name),
                new Claim(ClaimTypes.Role, "User")
            }, "JwtAuth"));
        }

        public async Task UpdateAuthenticationState(ClaimsPrincipal userClaimsPrincipal)
        {
            //// Check if the user is anonymous
            //if (userClaimsPrincipal.Identity?.IsAuthenticated == false || userClaimsPrincipal.IsInRole("Anonymous"))
            //{
            //    // Clear tokens for anonymous users
            //    await _tokenClientService.SetAccessTokenAsync("");
            //    await _tokenClientService.SetRefreshTokenAsync("");
            //}

            // Notify Blazor about the authentication state change
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(userClaimsPrincipal)));
        }

        private async Task<ClaimsPrincipal> ValidateAndRefreshTokenAsync()
        {
            try
            {
                // Retrieve the encrypted access token
                var jwtAccessTokenEncrypted = await _tokenClientService.GetAccessTokenAsync();

                // If no token exists, return anonymous
                if (string.IsNullOrEmpty(jwtAccessTokenEncrypted))
                    return _anonymousUser;

                // Decode the token
                var jwtAccessToken = JwtDecryptService.DecryptToken(jwtAccessTokenEncrypted);

                // Check token expiration
                if (jwtAccessToken.Expiration < DateTime.UtcNow)
                {
                    // Attempt to refresh tokens
                    var isTokenRefreshed = await _tokenClientService.RefreshTokensAsync();
                    if (!isTokenRefreshed)
                        return _anonymousUser;

                    // Retrieve and decode the refreshed token
                    jwtAccessTokenEncrypted = await _tokenClientService.GetAccessTokenAsync();
                    jwtAccessToken = JwtDecryptService.DecryptToken(jwtAccessTokenEncrypted);
                }

                // Return authenticated user if the token is valid
                return jwtAccessToken.Expiration >= DateTime.UtcNow
                    ? SetUserClaimPrincipal(jwtAccessToken)
                    : _anonymousUser;
            }
            catch
            {
                // On error, return anonymous
                return _anonymousUser;
            }
        }

        //public void UpdateAuthenticationStateOnLogin(string jwtAccessToken)
        //{
        //    var claimsPrincipal = new ClaimsPrincipal();

        //    if (!string.IsNullOrEmpty(jwtToken))
        //    {
        //        var getUserClaims = DecryptJWTService.DecryptToken(jwtToken);
        //        _currentUser = SetUserClaimPrincipal(getUserClaims);
        //    }

        //    test = true;

        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        //}




        //public ClaimsPrincipal SetAnonymousClaimPrincipal()
        //{
        //    return new ClaimsPrincipal(new ClaimsIdentity(new[]
        //    {
        //        new Claim(ClaimTypes.Role, "Anonymous")
        //    }, "JwtAuth"));
        //}

        //public void UpdateAuthenticationStateOnLogout(string jwtToken)
        //{
        //    var _currentUser = SetAnonymousClaimPrincipal();

        //    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
        //}

        //public string GetUserClaimPrincipalEmail()
        //{
        //    return _currentUser.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        //}

        //public string GetUserClaimPrincipalName()
        //{
        //    return _currentUser.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        //}


    }
}


