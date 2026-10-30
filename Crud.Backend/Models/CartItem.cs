using System.Text.Json.Serialization;

namespace Crud.Backend.Models;

public class CartItem
{
    public Guid Id { get; set; }
    
    // FK & Navigation to Cart
    public Guid CartId { get; set; }
    [JsonIgnore]
    public Cart Cart { get; set; } = null!;
    
    // FK & Navigation to Product
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; } = 1;
}
