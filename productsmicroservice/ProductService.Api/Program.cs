using FluentValidation.AspNetCore;
using ProductService.Api.Middlewares;
using ProductService.Application;
using ProductService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// FluentValidations
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

// authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
