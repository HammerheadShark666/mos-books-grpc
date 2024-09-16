using Microservice.Book.Grpc.Extensions;
using Microservice.Book.Grpc.Middleware;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.ConfigureGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureExceptionHandling();
builder.Services.ConfigureDI();
builder.Services.ConfigureSqlServer(builder.Configuration, environment);
builder.Services.ConfigureJwt();

var app = builder.Build();

app.ConfigureGrpc();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();