namespace WebAdminUI.States
{
    public static class Constants
    {
        public static string JWTToken { get; set; } = string.Empty;

        public static string JwtAccessTokenStorageKey { get; } = "AccessToken";

        public static string JwtRefreshTokenStorageKey { get; } = "RefreshToken";
    }
}
