using Crud.Backend.Models;
using Crud.Backend.Services;
using Microsoft.EntityFrameworkCore;

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
            await context.SaveChangesAsync();
        }

        if (!context.CartItems.Any())
        {
            var users = await context.Users.Include(u => u.Cart).ToListAsync();
            var products = await context.Products.ToListAsync();

            if (users.Count > 0 && products.Count > 0)
            {
                var jackCart = users.FirstOrDefault(u => u.Name == "Jack")?.Cart;
                var gibsCart = users.FirstOrDefault(u => u.Name == "Gibs")?.Cart;
                var jonathanCart = users.FirstOrDefault(u => u.Name == "Jonathan Joestar")?.Cart;

                var milk = products.FirstOrDefault(p => p.Name == "Milk");
                var coffee = products.FirstOrDefault(p => p.Name == "Cofee");
                var indomie = products.FirstOrDefault(p => p.Name == "Indomie Rendang");
                var kecap = products.FirstOrDefault(p => p.Name == "Kecap Bango");

                List<CartItem> cartItems = new();

                if (jackCart != null)
                {
                    if (milk != null) cartItems.Add(new CartItem { CartId = jackCart.Id, ProductId = milk.Id, Quantity = 2 });
                    if (indomie != null) cartItems.Add(new CartItem { CartId = jackCart.Id, ProductId = indomie.Id, Quantity = 5 });
                }

                if (gibsCart != null)
                {
                    if (coffee != null) cartItems.Add(new CartItem { CartId = gibsCart.Id, ProductId = coffee.Id, Quantity = 1 });
                    if (kecap != null) cartItems.Add(new CartItem { CartId = gibsCart.Id, ProductId = kecap.Id, Quantity = 1 });
                }

                if (jonathanCart != null)
                {
                    if (coffee != null) cartItems.Add(new CartItem { CartId = jonathanCart.Id, ProductId = coffee.Id, Quantity = 3 });
                    if (indomie != null) cartItems.Add(new CartItem { CartId = jonathanCart.Id, ProductId = indomie.Id, Quantity = 10 });
                }

                if (cartItems.Any())
                {
                    context.CartItems.AddRange(cartItems);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
