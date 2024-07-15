namespace BookStore.Model;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int Rating { get; set; }

    public Book(int id, string title, string author, int year, string description, string image, decimal price, int stock, int rating)
    {
        Id = id;
        Title = title;
        Author = author; Year = year; Description = description; Image = image;
        Price = price; Stock = stock; Rating = rating;
    }
}
