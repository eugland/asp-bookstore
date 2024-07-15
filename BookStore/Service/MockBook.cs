using BookStore.Model;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Service;

public class MockBook
{
    /// <summary>
    /// Create a list of mocked Books
    /// </summary>
    private static Random random = new Random();

    private static List<string> subjects = new List<string>
    {
        "The Cat", "A Wizard", "An Explorer", "A Detective", "The Knight",
        "A Robot", "The Queen", "A Pirate", "The Alien", "A Ghost"
    };

    private static List<string> verbs = new List<string>
    {
        "finds", "loses", "chases", "discovers", "investigates",
        "builds", "destroys", "captures", "escapes", "haunts"
    };

    private static List<string> objects = new List<string>
    {
        "a treasure", "a secret", "a mystery", "a dragon", "a spaceship",
        "an adventure", "a friend", "a kingdom", "a puzzle", "a monster"
    };

    private static List<string> firstNames = new List<string>
    {
        "John", "Jane", "Michael", "Emily", "Chris", "Jessica", "Matthew",
        "Laura", "Daniel", "Olivia", "David", "Sarah", "James", "Anna",
        "Robert", "Sophia", "William", "Emma", "Thomas", "Isabella"
    };

    private static List<string> lastNames = new List<string>
    {
        "Smith", "Doe", "Johnson", "Davis", "Brown", "Wilson", "White",
        "Green", "Thompson", "Martin", "Clark", "Lewis", "Walker", "Hall",
        "Allen", "Young", "King", "Wright", "Scott", "Adams"
    };

    /// <summary>
    /// Create a number of random books shall one wants to test the application, 
    /// This should read in the existing books it has
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="write_json"></param>
    /// <returns></returns>
    public static List<Book> GenerateBooks(int amount, bool write_json = false)
    {
        List<Book> books = new List<Book>();
        HashSet<int> usedIds = new HashSet<int>();

        for (int i = 0; i < amount; i++)
        {
            int id;
            do
            {
                id = random.Next(1, int.MaxValue);
            } while (usedIds.Contains(id));

            usedIds.Add(id);

            string title = $"{subjects[random.Next(subjects.Count)]} {verbs[random.Next(verbs.Count)]} {objects[random.Next(objects.Count)]}";
            string authorFirstName = firstNames[random.Next(firstNames.Count)];
            string authorLastName = lastNames[random.Next(lastNames.Count)];
            string author = $"{authorFirstName} {authorLastName}";
            string imageUrl = $"www.book.com/item/{author.Replace(" ", "")}{title.Replace(" ", "")}";

            Book book = new Book
            (
                id: id,
                title: title,
                author: author,
                year: random.Next(1900, DateTime.Now.Year + 1),
                description : $"A fascinating tale of {title.ToLower()}.",
                image : imageUrl,
                price: (decimal)Math.Round((random.NextDouble() * 99 + 1), 2),
                stock : random.Next(1, 100),
                rating: random.Next(1, 6)
            );

            books.Add(book);
        }

        return books;
    }
}
