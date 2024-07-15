using Microservice.Book.Api.Address.Api.Extensions;
using Microservice.Book.Gprc.Data.Contexts;
using Microservice.Book.Gprc.Data.Repository;
using Microservice.Book.Gprc.Data.Repository.Interfaces;
using Microservice.Book.Gprc.Middleware;
using Microservice.Book.Grpc.Helpers.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microservice.Book.Api.Extensions;

public static class IServiceCollectionExtensions
{
    public static void ConfigureExceptionHandling(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public static void ConfigureJwt(this IServiceCollection services)
    {
        services.AddJwtAuthentication();
    }

    public static void ConfigureDI(this IServiceCollection services)
    { 
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddMemoryCache();
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    } 

    public static void ConfigureDatabaseContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContextFactory<BookDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(Grpc.Helpers.Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()));
    } 

    public static void ConfigureGrpc(this IServiceCollection services)
    {
        services.AddGrpc(options =>
        {
            options.Interceptors.Add<ServerLoggerInterceptor>();
        });
        services.AddGrpcReflection();
        services.AddGrpc().AddJsonTranscoding();
    }
}