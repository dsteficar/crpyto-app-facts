namespace WebAdminUI.Services.Users
{
    using Microsoft.AspNetCore.Http;

    public class UserPreferencesService : IUserPreferencesService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserPreferencesService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetPreference(string key)
        {
            var context = _httpContextAccessor.HttpContext;
            return context?.Request.Cookies[key] ?? string.Empty;
        }

        public void SetPreference(string key, string value)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context == null) return;

            var cookieOptions = new CookieOptions
            {
                HttpOnly = false, // Can be accessed by client-side JS if needed
                Secure = true, // Enforce HTTPS
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)// Cross-site scenarios if needed
            };

            context.Response.Cookies.Append(key, value, cookieOptions);
        }
    }
}
