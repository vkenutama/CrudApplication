using Crud.Backend.Database;
using Crud.Backend.Models;
using Crud.Backend.Utils;
using Microsoft.EntityFrameworkCore;

namespace Crud.Backend.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<List<User>> GetAllUsers()
    {
        var users = await context.Users.ToListAsync();
        return users;
    }

    public async Task<User> GetUserById(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User tidak ditemukan");
        }
        
        return user;
    }

    public async Task<User> CreateUser(User user)
    {
        if (!user.ValidateUser())
        {
            throw new ArgumentException("Nama user ini tidak valid");
        }

        if (user.Cart == null)
        {
            user.Cart = new Cart();
        }

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User> UpdateUser(Guid id, User updateUser)
    {
        if (!updateUser.ValidateUser())
        {
            throw new ArgumentException("Data pengguna tidak valid");
        }

        var existingUser = await context.Users.FindAsync(id);

        if (existingUser == null)
        {
            throw new NotFoundException("User tidak ditemukan");
        }
        
        existingUser.Name = updateUser.Name;
        await context.SaveChangesAsync();
        
        return existingUser;
    }

    public async Task DeleteUser(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User tidak ditemukan");
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}
