using Azure.Core;
using Microsoft.EntityFrameworkCore;
public static class AdminSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().HasData(
            new Admin
            {
                AdminId = 1,
                SuperAdminId = 1,
                FirstName = "Shady",
                LastName = "Waleed",
                Email = "shady.waleed@gmail.com",
                Password = "temp",
                IsActive = true,
            },
            new Admin
            {
                AdminId = 2,
                SuperAdminId = 1,
                FirstName = "Mohamed",
                LastName = "Salah",
                Email = "mohamed.salah@gmail.com",
                Password = "adminpass123",
                IsActive = true,
            }
        );
    }
}
