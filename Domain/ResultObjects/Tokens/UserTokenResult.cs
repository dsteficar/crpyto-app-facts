using Domain.Entity.Authentication;

namespace Domain.ValueObjects.Tokens
{
    public class UserTokenResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set;  }
        public ApplicationUser User { get; set; }

        public UserTokenResult(string accessToken, string refreshToken, ApplicationUser user)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
        }
    }
}
