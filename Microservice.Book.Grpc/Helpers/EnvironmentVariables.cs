namespace Microservice.Book.Grpc.Helpers;

public class EnvironmentVariablesHelper
{
    public static string JwtIssuer = Environment.GetEnvironmentVariable(Constants.JwtIssuer);
    public static string JwtAudience = Environment.GetEnvironmentVariable(Constants.JwtAudience);
    public static string JwtSymmetricSecurityKey = Environment.GetEnvironmentVariable(Constants.JwtSymmetricSecurityKey);
}