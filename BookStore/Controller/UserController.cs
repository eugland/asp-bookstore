using BookStore.DBContext;
using BookStore.Model;
using BookStore.Service;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace BookStore.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly BookStoreDbContext _context;

    public UserController(BookStoreDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        var users = _context.Users.ToList();
        return Ok(users);
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User newUser)
    {
        var user = _context.Users.Add(newUser);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Entity.Id }, user);
    }
}
