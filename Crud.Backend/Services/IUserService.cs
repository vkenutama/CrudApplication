using Crud.Backend.Models;

namespace Crud.Backend.Services;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
    Task<User?> GetUserById(Guid id);
    Task<User?> CreateUser(User user);
    Task<User?> UpdateUser(Guid id, User updateUser);
    Task<bool> DeleteUser(Guid id);
}
