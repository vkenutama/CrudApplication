using Crud.Backend.Database;
using Crud.Backend.Dto;
using Crud.Backend.Models;
using Crud.Backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace Crud.Backend.Services;

public class ProductService(AppDbContext context) : IProductService
{
    public async Task<List<Product>> GetProducts()
    {
        var products = await context.Products.ToListAsync();
        return products;
    }

    public async Task<Product> GetProductById(Guid id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null)
        {
            throw new NotFoundException("Produk tidak ditemukan");
        }
        return product;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Nama produk tidak boleh kosong");
        }

        if (product.Price < 0)
        {
            throw new ArgumentException("Harga produk tidak boleh negatif");
        }

        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProduct(Guid id)
    {
        var existingProduct = await context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            throw new NotFoundException("Produk tidak ditemukan");
        }
        
        context.Products.Remove(existingProduct);
        await context.SaveChangesAsync();
    }

    public async Task<Product> UpdateProduct(Guid id, UpdateProductDto product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            throw new ArgumentException("Nama produk tidak boleh kosong");
        }

        if (product.Price < 0)
        {
            throw new ArgumentException("Harga produk tidak boleh negatif");
        }

        var existingProduct = await context.Products.FindAsync(id);

        if (existingProduct == null)
        {
            throw new NotFoundException("Produk tidak ditemukan");
        }
        
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;

        await context.SaveChangesAsync();
        return existingProduct;
    }
}
