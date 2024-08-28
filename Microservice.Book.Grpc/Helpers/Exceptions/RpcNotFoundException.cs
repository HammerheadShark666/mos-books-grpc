using Grpc.Core;

namespace Microservice.Book.Grpc.Helpers.Exceptions;

public class RpcNotFoundException(Status status) : RpcException(status)
{
}