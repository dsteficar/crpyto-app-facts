using Hangfire;
using Infrastructure.ServiceRegistration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;
using WebAdminUI.Components;
using WebAdminUI.Extensions;
using WebAdminUI.ServiceRegistration;
using Application.ServiceRegistration;
using WebAdminUI.Hubs;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazorServerServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Error handling and security
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts(); // HTTP Strict Transport Security
}

// Swagger
app.UseSwagger(); // Generate OpenAPI documentation
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FactsAdminBlazorServer v1");
});

// Authentication and authorization
app.UseAuthentication(); // Authenticate users
app.UseAuthorization(); // Enforce access control

//app.UseMiddleware<ErrorHandlingMiddleware>(); // Custom error handling middleware
app.UseHttpsRedirection(); // Enforce HTTPS
app.UseStaticFiles(); // Serve static files (e.g., CSS, JS)

// Routing and antiforgery
app.UseRouting(); // Enable endpoint routing
app.UseAntiforgery(); // Protect against CSRF attacks

// Session handling
app.UseSession(); // Enable session management

// Map Razor Components and Blazor Hubs
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapRazorPages();
app.MapControllers();
app.MapHub<TradingBotLogHub>("/tradingBotLogHub");

app.UseHangfireDashboard();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.Map("/hangfire", appBuilder =>
{
    appBuilder.UsePathBase("/hangfire");
    appBuilder.UseHangfireDashboard("/hangfire");
});

//// Idle timeout middleware
app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var lastActivity = context.Session.Get<DateTime?>("LastActivity");

        if (lastActivity != null && DateTime.UtcNow - lastActivity >= TimeSpan.FromHours(1)) // Idle timeout
        {
            await context.SignOutAsync("Cookies");
            context.Response.Redirect("/login");
        }

        context.Session.Set("LastActivity", DateTime.UtcNow);
    }

    await next();
});

// Final middleware
app.Run();