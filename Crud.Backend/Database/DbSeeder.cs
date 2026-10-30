using Crud.Backend.Models;

namespace Crud.Backend.Database;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
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
            
            context.Users.AddRange(users);
            context.SaveChanges();
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