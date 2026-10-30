using Crud.Backend.Models;
using Crud.Backend.Services;

namespace Crud.Backend.Database;

public static class DbSeeder
{
    public static async Task Seed(AppDbContext context, IUserService userService)
    {
        if (!context.Users.Any())
        {
            List<User> users =
            [
                new()
                {
                    Name = "Jack",
                },

                new()
                {
                    Name = "Gibs",
                },

                new()
                {
                    Name = "Jonathan Joestar",
                }

            ];

            foreach (var user in users)
            {
                await userService.CreateUser(user);
            }
        }

        if (!context.Products.Any())
        {
            List<Product> products =
            [
                new()
                {
                    Name = "Milk",
                    Price = 5_000,
                },
                new()
                {
                    Name = "Cofee",
                    Price = 9_000,
                },
                new()
                {
                    Name = "Indomie Rendang",
                    Price = 3_500,
                },
                new()
                {
                    Name = "Kecap Bango",
                    Price = 12_500,
                },
            ];
            
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}