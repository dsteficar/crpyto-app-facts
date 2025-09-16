using Application.Maps;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WebAdminUI.Services.Accounts;
using WebApplicationUI.Common.AppTheme;
using WebApplicationUI.Maps;
using WebApplicationUI.Services.JwtTokens;
using WebApplicationUI.Services.TradingBots;
using WebApplicationUI.Services.TradingViewGraph;
using WebApplicationUI.States;

namespace WebApplicationUI.ServiceRegistration
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddBlazorWasmServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7122/") });

            services.AddBlazoredLocalStorage();

            services.AddSingleton<AppTheme>();

            services.AddAuthorizationCore();

            services.AddCascadingAuthenticationState();

            services.AddScoped<Radzen.ThemeService>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IJwtTokenClientService, JwtTokenClientService>();
            services.AddScoped<ITradingViewGraphClientService, TradingViewGraphClientService>();
            services.AddScoped<IAccountClientService, AccountClientService>();
            services.AddScoped<ITradingBotClientService, TradingBotClientService>();
            services.AddScoped<ITradingBotLogService, TradingBotLogService>();

            services.AddAutoMapper(typeof(TradingBotAppProfile));
            services.AddAutoMapper(typeof(ParallelChannelProfile));

            return services;
        }
    }
}
