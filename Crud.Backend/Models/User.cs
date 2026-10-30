using System.ComponentModel.DataAnnotations;

namespace Crud.Backend.Models;

public class User
{
    public Guid Id { get; set; }
    
    [StringLength(30)]
    public string Name { get; set; } = String.Empty;

    public Cart? Cart { get; set; }
    
}