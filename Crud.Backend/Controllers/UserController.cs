using Crud.Backend.Dto;
using Crud.Backend.Models;
using Crud.Backend.Services;
using Crud.Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var users = await userService.GetAllUsers();
            return ApiResponse(users, "Berhasil mengambil data user");
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
            var user = await userService.GetUserById(id);
            return ApiResponse(user, "Berhasil mengambil data user");
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
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        try
        {
            var newUser = new User
            {
                Name = dto.Name,
            };

            var createdUser = await userService.CreateUser(newUser);
            return ApiResponse(createdUser, "Berhasil membuat pengguna baru");
        }
        catch (ArgumentException ex)
        {
            return ApiError(ex.Message);
        }
        catch (Exception ex)
        {
            return ApiError(ex.Message, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] PutUserDto dto, Guid id)
    {
        try
        {
            var updatedUser = new User
            {
                Name = dto.Name,
            };

            var result = await userService.UpdateUser(id, updatedUser);
            return ApiResponse(result, "Berhasil memperbarui data pengguna");
        }
        catch (NotFoundException ex)
        {
            return ApiError(ex.Message, StatusCodes.Status404NotFound);
        }
        catch (ArgumentException ex)
        {
            return ApiError(ex.Message);
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
            await userService.DeleteUser(id);
            return ApiResponse<object>(null, "Berhasil menghapus pengguna");
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
