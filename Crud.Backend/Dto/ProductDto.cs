namespace Crud.Backend.Dto;

public record UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public record CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}