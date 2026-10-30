using Crud.Backend.Dto;
using Crud.Backend.Models;

namespace Crud.Backend.Services;

public interface IProductService
{
    Task<List<Product>> GetProducts();
    Task<Product> GetProductById(Guid id);
    Task<Product> CreateProduct(Product product);
    Task<Product> UpdateProduct(Guid id, UpdateProductDto product);
    Task DeleteProduct(Guid id);
}
