using Microservice.Book.Grpc.Service;

namespace Microservice.Book.Grpc.Extensions;

public static class AppExtensions
{
    public static void ConfigureGrpc(this WebApplication webApplication)
    {
        webApplication.MapGrpcService<BookService>();
        webApplication.MapGrpcReflectionService();
    }
}