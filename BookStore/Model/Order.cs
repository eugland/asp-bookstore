namespace BookStore.Model;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime Date { get; set; }

    public Order()
    {
    }
}
