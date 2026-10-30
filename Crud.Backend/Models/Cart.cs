using System.Text.Json.Serialization;

namespace Crud.Backend.Models;

public class Cart
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    
    [JsonIgnore]
    public User User { get; set; } = null!;

    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}
