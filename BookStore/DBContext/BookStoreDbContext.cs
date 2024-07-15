using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBContext;

public class BookStoreDbContext: DbContext
{
    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
}
