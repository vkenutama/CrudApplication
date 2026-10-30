using Crud.Backend.Models;

namespace Crud.Backend.Utils;

public static class Validator
{
    public static bool ValidateUser(this User user)
    {
        // Check if name is not empty
        if (string.IsNullOrEmpty(user.Name))
            return false;

        if (user.Name.Length > 30)
            return false;
        
        return true;
    }
}