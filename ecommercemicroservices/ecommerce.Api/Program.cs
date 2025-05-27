using ecommerce.Infrastructure;
using ecommerce.Core;
using ecommerce.Api.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddInfrastructure();
builder.Services.AddCoreServices();



var app = builder.Build();

app.UseExceptionHandlingMiddleware();
// Configure the HTTP request pipeline.
app.UseRouting();

// Configure Authentication and Authorization if needed
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();


app.Run();
