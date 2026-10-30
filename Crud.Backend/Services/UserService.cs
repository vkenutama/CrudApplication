using Crud.Backend.Database;
using Crud.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.Backend.Services;

public class UserService(AppDbContext context) : IUserService
{
    public async Task<List<User>> GetAllUsers()
    {
        var users = await context.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        return user;
    }

    public async Task<User?> CreateUser(User user)
    {
        if (user.Cart == null)
        {
            user.Cart = new Cart();
        }

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> UpdateUser(Guid id, User updateUser)
    {
        var existingUser = await context.Users.FindAsync(id);

        if (existingUser == null)
            return null;
        
        existingUser.Name = updateUser.Name;
        await context.SaveChangesAsync();
        
        return existingUser;
    }

    public async Task<bool> DeleteUser(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
            return false;

        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return true;
    }
}
