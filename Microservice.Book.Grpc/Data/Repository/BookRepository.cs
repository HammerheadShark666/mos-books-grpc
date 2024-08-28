using Microservice.Book.Grpc.Data.Context;
using Microservice.Book.Grpc.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.Book.Grpc.Data.Repository;

public class BookRepository(IDbContextFactory<BookDbContext> dbContextFactory) : IBookRepository
{
    public IDbContextFactory<BookDbContext> _dbContextFactory { get; set; } = dbContextFactory;

    public async Task<Domain.Book> ByIdAsync(Guid id)
    {
        await using var db = await _dbContextFactory.CreateDbContextAsync();
        return await db.Books
                        .Where(o => o.Id.Equals(id))
                        .Include(e => e.Author)
                        .Include(e => e.Publisher)
                        .Include(e => e.Series)
                        .Include(e => e.DiscountType)
                        .SingleOrDefaultAsync();
    }
}