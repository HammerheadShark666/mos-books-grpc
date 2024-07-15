using Microservice.Book.Api.Extensions;
using Microservice.Book.Gprc.Middleware;
using Microservice.Book.Grpc.Extensions;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.ConfigureDatabaseContext(builder.Configuration); 
builder.Services.ConfigureJwt(); 

var app = builder.Build();

app.ConfigureGprc(); 
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlingMiddleware>(); 

app.Run();