using Microsoft.EntityFrameworkCore;
public static class AddressSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>().HasData(
            new Address
            {
                AddressId = 1,
                City = "Giza",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 2,
                City = "Cairo",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 3,
                City = "Hurghada",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 4,
                City = "Luxor",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 5,
                City = "Aswan",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 6,
                City = "Hurghada",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 7,
                City = "Sharm ElSheikh",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 8,
                City = "Ain Sokhna",
                Country = "Egypt",
            },
            new Address
            {
                AddressId = 9,
                City = "Ras Sedr",
                Country = "Egypt",
            }
        );
    }
}
