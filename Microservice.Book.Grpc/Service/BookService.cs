using Grpc.Core;
using Microservice.Book.Grpc.Data.Repository.Interfaces;
using Microservice.Book.Grpc.Protos;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.Book.Grpc.Service;

public class BookService(IBookRepository bookRepository) : BookGrpc.BookGrpcBase
{
    private readonly IBookRepository _bookRepository = bookRepository;

    [Authorize]
    public override async Task<BooksResponse> GetBooks(ListBookRequest request, ServerCallContext context)
    {
        BooksResponse books = new();

        foreach (var bookRequest in request.BookRequests)
        {
            var book = await _bookRepository.ByIdAsync(new Guid(bookRequest.Id));
            if (book != null)
            {
                books.BookResponses.Add(new BookResponse()
                {
                    Id = bookRequest.Id,
                    Name = book.Title,
                    UnitPrice = book.Price.ToString()
                });
            }
            else
            {
                books.NotFoundBookResponses.Add(new NotFoundBookResponse() { Id = bookRequest.Id });
            }
        }

        return books;
    }
}