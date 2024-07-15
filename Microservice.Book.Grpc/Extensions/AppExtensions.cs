namespace Microservice.Book.Grpc.Extensions;

public static class AppExtensions
{
    public static void ConfigureGprc(this WebApplication app)
    {
        app.MapGrpcService<BookService>();
        app.MapGrpcReflectionService();
    }
}
