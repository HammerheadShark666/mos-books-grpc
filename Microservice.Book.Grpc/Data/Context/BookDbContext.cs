using Microsoft.EntityFrameworkCore;

namespace Microservice.Book.Gprc.Data.Contexts;

public class BookDbContext : DbContext
{ 
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
 
    public DbSet<Grpc.Domain.Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);
    }
}

//add-migration
//update-database

//azurite --silent --location c:\azurite --debug c:\azurite\debug.log