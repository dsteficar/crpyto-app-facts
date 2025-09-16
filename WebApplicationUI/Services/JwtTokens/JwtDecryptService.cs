using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplicationUI.States;

namespace WebApplicationUI.Services.JwtTokens
{
    public static class JwtDecryptService
    {
        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            try
            {
                if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaims();

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(jwtToken);

                var id = jsonToken.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);
                var name = jsonToken.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Name);
                var email = jsonToken.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);
                var expClaim = jsonToken.Claims.FirstOrDefault(_ => _.Type == JwtRegisteredClaimNames.Exp);
                var expDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim!.Value)).UtcDateTime;

                return new CustomUserClaims(id!.Value, name!.Value, email!.Value, expDate);
            }
            catch
            {
                return null!;
            }

        }
    }
}
