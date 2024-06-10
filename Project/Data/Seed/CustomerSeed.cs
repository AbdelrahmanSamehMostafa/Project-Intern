using Microsoft.EntityFrameworkCore;

public static class CustomerSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                CustomerId = 1,
                FirstName = "Eslam",
                LastName = "Waleed",
                Email = "eslam.waleed@gmail.com",
                Password = "temp",
                Wishlists = new List<string> { "1", "2", "3" },
                IsEmailVerified = true,
            },
            new Customer
            {
                CustomerId = 2,
                FirstName = "Abdelrahman",
                LastName = "Sameh",
                Email = "abdelrahman.sameh@gmail.com",
                Password = "password123",
                Wishlists = new List<string> { "1", "2" },
                IsEmailVerified = true,
            },
            new Customer
            {
                CustomerId = 3,
                FirstName = "Ahmed",
                LastName = "Mohamed",
                Email = "Ahmed.Mohamed@gmail.com",
                Password = "password123",
                Wishlists = new List<string> { "4", "5" },
                IsEmailVerified = true,
            },
            new Customer
            {
                CustomerId = 4,
                FirstName = "test",
                LastName = "test",
                Email = "temp@gmail.com",
                Password = "temp",
                Wishlists = new List<string> { "4", "5" },
                IsEmailVerified = true,
            }
        );
    }
}
