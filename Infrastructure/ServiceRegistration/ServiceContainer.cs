using Application.Contracts.Repos;
using Application.Contracts.Services;
using Domain.Entity.Authentication;
using Infrastructure.Data;
using Infrastructure.Repos;
using Infrastructure.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Infrastructure.ServiceRegistration
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));

            services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationUserRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager();

            services.AddDataProtection()
                .PersistKeysToDbContext<AppDbContext>()
                .SetApplicationName("Facts");

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITokenRepository, RefreshTokenRepository>();
            services.AddScoped<IParallelChannelRepository, ParallelChannelRepository>();
            services.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
            services.AddScoped<ITradeBotTaskRepository, TradingBotTaskRepository>();
            services.AddScoped<ITradeBotSettingsRepository, TradingBotSettingsRepository>();
            services.AddScoped<IBinanceService, BinanceService>();  

            services.AddHttpClient<IBinanceService, BinanceService>(client =>
            {
                client.BaseAddress = new Uri("https://fapi.binance.com/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{

            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = configuration["Jwt:Issuer"],
            //        ValidAudience = configuration["Jwt:Audience"],
            //        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            //services.AddAuthorization();


            //services.AddCors(options =>
            //{
            //    options.AddPolicy("WebUI",
            //        builder => builder
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .WithOrigins("https://localhost:5001")
            //            .AllowCredentials());
            //});

            return services;
        }
    }
}
