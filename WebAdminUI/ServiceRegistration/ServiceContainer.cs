using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using WebAdminUI.Common.AppTheme;
using WebAdminUI.Maps;
using WebAdminUI.Services.Accounts;
using WebAdminUI.Services.Bots;
using WebAdminUI.Services.Users;
using WebAdminUI.States;

namespace WebAdminUI.ServiceRegistration
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddBlazorServerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorComponents()
             .AddInteractiveServerComponents();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "FactsAdminBlazorServer", Version = "v1" });
            });

            services.AddHangfire(hangfireConfiguration => hangfireConfiguration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("Default"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();
            services.AddHttpContextAccessor();
            services.AddSignalR();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7277") });
            services.AddScoped<IAccountClientService, AccountClientService>();
            services.AddScoped<IUserPreferencesService, UserPreferencesService>();
            services.AddHostedService<TradeBotTaskCheckBackgroundService>();
            services.AddScoped<ITradeBotScheduledTaskService, TradeBotScheduledTaskService>();

            services.AddAutoMapper(typeof(UserAdminUIProfile));
            services.AddAutoMapper(typeof(ParallelChannelUIAdminProfile));

            services.AddRazorPages().WithRazorPagesRoot("/Components/Pages");
            services.AddServerSideBlazor();

            services.AddAntiforgery(options =>
            {
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Enforce HTTPS
                options.Cookie.SameSite = SameSiteMode.None; // Cross-origin compatibility
            });

            services.AddAuthentication("Cookies")
            .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Enforce HTTPS
                options.Cookie.SameSite = SameSiteMode.None;
                options.ExpireTimeSpan = TimeSpan.FromHours(1); // Set cookie expiration
                options.SlidingExpiration = true; // Renew cookie automatically
                options.LoginPath = "/login";
                options.LogoutPath = "/login";
            });

            services.AddSession(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Enforce HTTPS
                options.Cookie.SameSite = SameSiteMode.None;// Required for GDPR compliance
            });

            services.AddCascadingAuthenticationState();
            services.AddAuthorization();
            services.AddScoped<AppThemeService>();
            return services;
        }
    }
}