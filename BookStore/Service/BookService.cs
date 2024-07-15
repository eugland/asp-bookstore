using BookStore.DBContext;
using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service;

public class BookService(BookStoreDbContext Ctx)
{
    
    public DbSet<Book> GetBook()
    {
        return Ctx.Books;
    }

    public Book? GetBookById(int id)
    {
        return Ctx.Books.Find(id);
    }

    public IEnumerable<Book> GetBooks()
    {
        return Ctx.Books.AsQueryable();
    }

    public IEnumerable<Book> SearchBooks(string title)
    {
        return Ctx.Books.Where(b => b.Title.Contains(title)).ToList();
    }

    public IEnumerable<Book> GenerateBooks(int amount)
    {
        var books = MockBook.GenerateBooks(amount);
        Ctx.Books.AddRange(books);
        Ctx.SaveChanges();
        return books;
    }
}
