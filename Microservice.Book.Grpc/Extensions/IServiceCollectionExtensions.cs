using Microservice.Book.Grpc.Data.Context;
using Microservice.Book.Grpc.Data.Repository;
using Microservice.Book.Grpc.Data.Repository.Interfaces;
using Microservice.Book.Grpc.Helpers.Interceptors;
using Microservice.Book.Grpc.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microservice.Book.Grpc.Extensions;

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
            options.UseSqlServer(configuration.GetConnectionString(Helpers.Constants.DatabaseConnectionString),
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