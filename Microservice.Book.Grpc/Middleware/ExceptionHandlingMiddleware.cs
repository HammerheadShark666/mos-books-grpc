using Microservice.Book.Grpc.Helpers.Exceptions;
using System.Text.Json;

namespace Microservice.Book.Grpc.Middleware;

internal sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        switch (statusCode)
        {
            case 404:
                {
                    var response = new
                    {
                        status = statusCode,
                        detail = exception.Message
                    };

                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                    break;
                }
            default:
                {
                    var response = new
                    {
                        status = statusCode,
                        detail = exception.Message
                    };

                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
                    break;
                }
        }
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };
}