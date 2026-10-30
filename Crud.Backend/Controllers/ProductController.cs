using Crud.Backend.Dto;
using Crud.Backend.Models;
using Crud.Backend.Services;
using Crud.Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await productService.GetProducts();
            return ApiResponse(products, "Berhasil mengambil data produk");
        }
        catch (Exception ex)
        {
            return ApiError(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var product = await productService.GetProductById(id);
            return ApiResponse(product, "Berhasil mengambil data produk");
        }
        catch (NotFoundException ex)
        {
            return ApiError(ex.Message, StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            return ApiError(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
    {
        try
        {
            var newProduct = new Product
            {
                Name = dto.Name,
                Price = dto.Price
            };

            var createdProduct = await productService.CreateProduct(newProduct);
            return ApiResponse(createdProduct, "Berhasil membuat produk baru");
        }
        catch (ArgumentException ex)
        {
            return ApiError(ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            return ApiError(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] UpdateProductDto dto, Guid id)
    {
        try
        {
            var updatedProduct = await productService.UpdateProduct(id, dto);
            return ApiResponse(updatedProduct, "Berhasil memperbarui data produk");
        }
        catch (NotFoundException ex)
        {
            return ApiError(ex.Message, StatusCodes.Status404NotFound);
        }
        catch (ArgumentException ex)
        {
            return ApiError(ex.Message, StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            return ApiError(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await productService.DeleteProduct(id);
            return ApiResponse<object>(null, "Berhasil menghapus produk");
        }
        catch (NotFoundException ex)
        {
            return ApiError(ex.Message, StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            return ApiError(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }
}
