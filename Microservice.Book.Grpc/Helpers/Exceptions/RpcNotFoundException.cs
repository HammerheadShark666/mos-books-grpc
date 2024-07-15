using Grpc.Core;

namespace Microservice.Book.Grpc.Helpers.Exceptions;

public class RpcNotFoundException : RpcException
{
    public RpcNotFoundException(Status status) : base(status)
    {
    }
}