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
                isAvailable = true
            },
            new Room
            {
                HotelId = 1,
                RoomId = 2,
                RoomType = RoomType.Double,
                Description = "A Triple room",
                Price = 400,
                isAvailable = true
            }
        );
        modelBuilder.Entity<Room>()
        .Property(u => u.RoomType)
        .HasConversion<string>();;
    }
}
