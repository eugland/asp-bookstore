using BookStore.DBContext;
using BookStore.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging();
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase("BookStore"));
builder.Services.AddHostedService<DatabaseInitializer>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<MockBook>();
builder.Services.AddControllers();
var app = builder.Build();


// sanity check 1: UseDeveloperException basically returns a exception page
// when you deploy the application in a dev environment.
// An example of a dev environment is when you run the application in Visual Studio.
// Then head to /underfined

app.UseExceptionHandler("/error");

app.MapGet("/", () => "Hello World!");
app.MapControllers();
//app.UseStatusCodePages();

app.Run();
