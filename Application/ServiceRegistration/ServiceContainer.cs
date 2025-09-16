using Application.Contracts.Services;
using Application.Maps;
using Application.Services.Account;
using Application.Services.Tokens;
using Application.Services.Trading.Bots;
using Application.Services.Trading.Graphs;
using Application.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.ServiceRegistration
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            //Mapper profiles
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(UserAdminProfile));
            services.AddAutoMapper(typeof(ParallelChannelProfile));
            services.AddAutoMapper(typeof(TradeBotProfile));

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IParallelChannelService, ParallelChannelService>();
            services.AddScoped<ITradingDataService, TradingDataService>();
            services.AddScoped<IUserSettingsService, UserSettingsService>();
            services.AddScoped<ITradingBotSettingsService, TradingBotSettingsService>();
            services.AddScoped<ITradingBotTaskService, TradingBotTaskService>();
            return services;
        }
    }

    //public static IServiceCollection AddApplicationService(this IServiceCollection services)
    //{
    //services.AddScoped<IUserApiClient, UserApiClient>();
    //services.AddScoped<IAccountApiClient, AccountApiClient>();
    //services.AddAuthorizationCore();
    //services.AddNetcodeHubLocalStorageService();
    //services.AddScoped<Extensions.LocalStorageService>();
    //services.AddScoped<HttpClientService>();

    //services.AddScoped<IAccountService, AccountService>();

    //services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
    //services.AddTransient<CustomHttpHandler>();
    //services.AddCascadingAuthenticationState();
    //services.AddHttpClient("WebUIClient", client =>
    //{
    //    client.BaseAddress = new Uri("https://localhost:7155");
    //}).AddHttpMessageHandler<CustomHttpHandler>();
    //    return services;
    //}

    //public static IServiceCollection AddApiClientServices(this IServiceCollection services)
    //{
    //    services.AddScoped<IUserApiClient, UserApiClient>();
    //    //services.AddScoped<HttpClientService>();
    //    //services.AddHttpClient("AdminWebUIClient", client =>
    //    //{
    //    //    client.BaseAddress = new Uri("https://localhost:7155");
    //    //}).AddHttpMessageHandler<CustomHttpHandler>();
    //}
    //}
}
