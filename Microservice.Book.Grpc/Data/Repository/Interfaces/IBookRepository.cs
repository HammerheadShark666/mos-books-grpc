namespace Microservice.Book.Gprc.Data.Repository.Interfaces;

public interface IBookRepository
{ 
    Task<Grpc.Domain.Book> ByIdAsync(Guid id);  
}