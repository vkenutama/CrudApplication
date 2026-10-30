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
        var users = await userService.GetAllUsers();
        return ApiResponse(users, "Berhasil mengambil data user");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetUserById(id);

        if (user == null)
        {
            return ApiError("Tidak ditemukan user dengan id ini", StatusCodes.Status404NotFound);
        }

        return ApiResponse(user, "Berhasil mengambil data user");
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        User newUser = new User
        {
            Name = dto.Name,
        };

        bool valid = newUser.ValidateUser();

        if (!valid)
            return ApiError("Nama user ini tidak valid", StatusCodes.Status400BadRequest);

        var createdUser = await userService.CreateUser(newUser);
        return ApiResponse(createdUser, "Berhasil membuat pengguna baru");
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromBody] PutUserDto dto, Guid id)
    {
        var updatedUser = new User
        {
            Name = dto.Name,
        };

        bool valid = updatedUser.ValidateUser();
        if (!valid)
            return ApiError("Data pengguna tidak valid", StatusCodes.Status400BadRequest);
        
        var result = await userService.UpdateUser(id, updatedUser);

        if (result == null)
            return ApiError("Pengguna ini tidak ditemukan", StatusCodes.Status404NotFound);

        return ApiResponse(result, "Berhasil memperbarui data pengguna");
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var isDeleted = await userService.DeleteUser(id);

        if (!isDeleted)
            return ApiError("Tidak ditemukan user dengan id ini", StatusCodes.Status404NotFound);

        return ApiResponse<object>(null, "Berhasil menghapus pengguna");
    }
}
