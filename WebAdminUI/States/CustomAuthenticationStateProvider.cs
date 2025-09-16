using Domain.Entity.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace WebAdminUI.States
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ClaimsPrincipal _anonymousUser = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role, "Anonymous")
            }, "CookieAuth"));

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity is not null && httpContext.User.Identity.IsAuthenticated)
            {
                var claimsPrincipal = new ClaimsPrincipal(httpContext.User);
                return Task.FromResult(new AuthenticationState(claimsPrincipal));
            }

            return Task.FromResult(new AuthenticationState(_anonymousUser));
        }


        public async Task SetUserClaimPrincipal(ApplicationUser user)
        {
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Name)) return;

                var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim(ClaimTypes.Name,  user.Name!),
                };

            var claimsIdentity = new ClaimsIdentity(userClaims, "CookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //var httpContext = _httpContextAccessor.HttpContext;

            //if (httpContext is null) return;

            //await httpContext.SignInAsync("Cookies", claimsPrincipal);

            var test = "test";

            //if (claims.Email is null) return new ClaimsPrincipal();

            //return new ClaimsPrincipal(new ClaimsIdentity(new[]
            //{
            //    new Claim(ClaimTypes.Email, claims.Email),
            //    new Claim(ClaimTypes.Name, claims.Name),
            //}, "CookieAuth"));
        }

        public void UpdateAuthenticationState(bool isForLogout)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            //if (isForLogout)
            //{
            //    // Clear cookies or session for logout
            //    _httpContextAccessor.HttpContext?.SignOutAsync("Cookies");
            //    claimsPrincipal = _anonymousUser;
            //}
            //else
            //{
            //    var httpContext = _httpContextAccessor.HttpContext;

            //    if (httpContext?.User?.Identity is not null && httpContext.User.Identity.IsAuthenticated)
            //    {
            //        // Use the existing authenticated user's claims
            //        claimsPrincipal = new ClaimsPrincipal(httpContext.User);
            //    }
            //    else
            //    {
            //        // If no authenticated user, set as anonymous
            //        claimsPrincipal = _anonymousUser;
            //    }
            //}

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
