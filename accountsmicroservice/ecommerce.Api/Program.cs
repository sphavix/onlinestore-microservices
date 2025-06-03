using ecommerce.Infrastructure;
using ecommerce.Core;
using ecommerce.Api.Middlewares;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddInfrastructure();
builder.Services.AddCoreServices();

// FluentValidations
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Ecommerce API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName); // Use full name for schema IDs to avoid conflicts
});

var app = builder.Build();

app.UseExceptionHandlingMiddleware();
// Configure the HTTP request pipeline.
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

// Use CORS if needed
app.UseCors();
// Configure Authentication and Authorization if needed
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();


app.Run();
