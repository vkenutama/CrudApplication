namespace Crud.Backend.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}