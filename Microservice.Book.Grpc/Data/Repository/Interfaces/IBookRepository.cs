namespace Microservice.Book.Grpc.Data.Repository.Interfaces;

public interface IBookRepository
{
    Task<Domain.Book> ByIdAsync(Guid id);
}