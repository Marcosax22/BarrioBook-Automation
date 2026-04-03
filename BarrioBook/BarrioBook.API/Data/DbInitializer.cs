using BarrioBook.Domain.Entities;
using BarrioBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BarrioBook.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(BarrioBookDbContext context)
        {
            await context.Database.MigrateAsync();

            const string adminEmail = "admin@barriobook.com";
            const string adminPassword = "Admin123*";

            var existingAdmin = await context.Customers
                .FirstOrDefaultAsync(c => c.Email == adminEmail);

            if (existingAdmin == null)
            {
                var admin = new Customer(
                    name: "Administrador BarrioBook",
                    email: adminEmail,
                    passwordHash: BCrypt.Net.BCrypt.HashPassword(adminPassword),
                    phone: "8090000000",
                    role: "Admin"
                );

                context.Customers.Add(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}