using Infrastructure.ServiceRegistration;
using WebAdminUI.Middleware;
using WebApi.ServiceRegistration;
using Application.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebApiServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error", createScopeForErrors: true);
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("WebAPI");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
