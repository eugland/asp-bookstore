using BookStore.Model;
using BookStore.Service;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BookStore.Controller;

[Route("api/[controller]")]
[ApiController]
public class BookController (BookService bookService) : ControllerBase
{

    [HttpGet]
    public IResult GetBooks([FromQuery]string? Title, [FromQuery]string? Author)
    {
        
        if (Title != null)
        {
            
        }
        var books = bookService.GetBooks().ToList();
        if (books == null)
        {
            return TypedResults.NotFound("The BookShelf is Empty, try adding a book!");
        }
        return TypedResults.Ok<IEnumerable<Book>>(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        var book = bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    // search a book by title
    [HttpGet("search")]
    public IActionResult SearchBooks([FromQuery] string title)
    {
        return Ok($"Hello from BookController with search title: {title}");
    }

    [HttpGet("create")]
    public IResult CreateBook([FromQuery] int amount = 10)
    {
        // Handle default values or null checks
        if (amount < 1)
        {
            return Results.BadRequest("Amount must be greater than 0");
        }
        var books = bookService.GenerateBooks(amount);
        return TypedResults.Created<IEnumerable<Book>>("api/book", books);
    }
}
