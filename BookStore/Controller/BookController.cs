using BookStore.Model;
using BookStore.Service;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controller;

[Route("api/[controller]")]
[ApiController]
public class BookController (BookService bookService) : ControllerBase
{

    [HttpGet]
    public IResult GetBooks(
        [FromQuery]string? Title, 
        [FromQuery]string? Author, 
        [FromQuery]int? minPrice, 
        [FromQuery]int? maxPrice,
        [FromQuery]bool? inStock,
        [FromQuery]int? minRating,
        [FromQuery]int? maxRating,
        [FromQuery]int? year)
    {

        Func<Book, bool> filter = book =>
        (string.IsNullOrEmpty(Author) || book.Author.Contains(Author)) &&
        (string.IsNullOrEmpty(Title) || book.Title.Contains(Title)) &&
        (!year.HasValue || book.Year == year) &&
        (!minPrice.HasValue || book.Price >= minPrice) &&
        (!maxPrice.HasValue || book.Price <= maxPrice) &&
        (!minRating.HasValue || book.Rating >= minRating) &&
        (!maxRating.HasValue || book.Rating <= maxRating) &&
        (!inStock.HasValue || (inStock.Value && book.Stock > 0));


        var books = bookService.GetBooks().Where(filter);
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
