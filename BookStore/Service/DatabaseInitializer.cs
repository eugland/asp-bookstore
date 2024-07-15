using BookStore.Constant;
using BookStore.DBContext;
using BookStore.Model;
using System.Text.Json;

namespace BookStore.Service;

public class DatabaseInitializer(IServiceProvider sp, ILogger<DatabaseInitializer> log): IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = sp.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();

        await ctx.Database.EnsureCreatedAsync(cancellationToken);

        if (!ctx.Books.Any())
        {
            // Read JSON file
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(
                File.ReadAllText("Data/books.json"),
                JsonOptions.CaseNeutralCamelJson);
            if (books != null)
            {
                log.LogInformation("Seeding database with {Count} books with json file", books.Count());
                ctx.Books.AddRange(books);
                await ctx.SaveChangesAsync(cancellationToken);
            }
        }
        if (!ctx.Books.Any())
        {
            var books = MockBook.GenerateBooks(10);
            log.LogInformation("Seeding database with {Count} books with random books generator", books.Count());
            await File.WriteAllTextAsync("Data/books.json", 
                JsonSerializer.Serialize(books, new JsonSerializerOptions() 
                { 
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                }));
            ctx.Books.AddRange(books);
            await ctx.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
