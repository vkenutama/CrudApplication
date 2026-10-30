using Crud.Backend.Database;
using Crud.Backend.Dto;
using Crud.Backend.Models;
using Crud.Backend.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(AppDbContext context) : BaseApiController
{
    [HttpGet]
    public IActionResult GetAll()
    {
        var users = context.Users.ToList();

        return ApiResponse(users, "Berhasil mengambil data user");
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        var user = context.Users.Find(id);

        if (user == null)
        {
            return ApiError("Tidak ditemukan user dengan id ini", StatusCodes.Status404NotFound);
        }

        return ApiResponse(user, "Berhasil mengambil data user");
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateUserDto dto)
    {
        User newUser = new User
        {
            Name = dto.Name,
        };

        bool valid = newUser.ValidateUser();

        if (!valid)
            return ApiError("Nama user ini tidak valid", StatusCodes.Status400BadRequest);

        context.Users.Add(newUser);
        context.SaveChanges();

        return ApiResponse(newUser, "Berhasil membuat pengguna baru");
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromBody] PutUserDto dto, Guid id)
    {
        var existingUser = context.Users.Find(id);

        if (existingUser == null)
            return ApiError("Pengguna ini tidak ditemukan", StatusCodes.Status404NotFound);

        var updatedUser = new User
        {
            Name = dto.Name,
        };

        bool valid = updatedUser.ValidateUser();
        if (!valid)
            return ApiError("Data pengguna tidak valid", StatusCodes.Status400BadRequest);

        existingUser.Name = updatedUser.Name;
        context.SaveChanges();

        return ApiResponse(updatedUser, "Berhasil memperbarui data pengguna");
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var userToDelete = context.Users.Find(id);

        if (userToDelete == null)
            return ApiError("Tidak ditemukan user dengan id ini", StatusCodes.Status404NotFound);

        context.Users.Remove(userToDelete);
        context.SaveChanges();

        return ApiResponse<object>(null, "Berhasil menghapus pengguna");
    }
}