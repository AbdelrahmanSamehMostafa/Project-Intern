using Microsoft.EntityFrameworkCore;

public static class RoomSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room
            {
                HotelId = 1,
                RoomId = 1,
                RoomType = RoomType.Single,
                Description = "A deluxe room",
                Price = 200,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 1,
                RoomId = 2,
                RoomType = RoomType.Double,
                Description = "A double room",
                Price = 400,
                isAvailable = false,
                isBooked = true
            },
            new Room
            {
                HotelId = 2,
                RoomId = 3,
                RoomType = RoomType.Single,
                Description = "A single room",
                Price = 150,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 2,
                RoomId = 4,
                RoomType = RoomType.Triple,
                Description = "A triple room",
                Price = 500,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 3,
                RoomId = 5,
                RoomType = RoomType.Quadra,
                Description = "A quadra room",
                Price = 600,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 3,
                RoomId = 6,
                RoomType = RoomType.Single,
                Description = "A single room",
                Price = 150,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 4,
                RoomId = 7,
                RoomType = RoomType.Single,
                Description = "A single room",
                Price = 150,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 4,
                RoomId = 8,
                RoomType = RoomType.Double,
                Description = "A double room",
                Price = 300,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 5,
                RoomId = 9,
                RoomType = RoomType.Triple,
                Description = "A triple room",
                Price = 450,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 5,
                RoomId = 10,
                RoomType = RoomType.Quadra,
                Description = "A quadra room",
                Price = 600,
                isAvailable = true,
                isBooked = true
            },
            new Room
            {
                HotelId = 6,
                RoomId = 11,
                RoomType = RoomType.Quadra,
                Description = "A quadra room",
                Price = 600,
                isAvailable = true,
                isBooked = false
            },
            new Room
            {
                HotelId = 7,
                RoomId = 12,
                RoomType = RoomType.Quadra,
                Description = "A quadra room",
                Price = 600,
                isAvailable = true,
                isBooked = false
            }
        );

        modelBuilder.Entity<Room>()
            .Property(u => u.RoomType)
            .HasConversion<string>();
    }
}
