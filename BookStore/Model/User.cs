namespace BookStore.Model;

public class User
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string Role { get; init; }
    public decimal Balance { get; init; }

    public User(string name, string email, string password, string role="User", decimal balance = 0)
    {
        Name = name; 
        Email = email; 
        Password = password; 
        Role = role;
        Balance = balance;
    }
}
