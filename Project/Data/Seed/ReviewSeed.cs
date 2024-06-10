using Microsoft.EntityFrameworkCore;
public static class ReviewSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Review>().HasData(
            new Review
            {
                ReviewId = 1,
                Comment = "Great stay!",
                Rating = 8,
                CustomerId = 1,
                HotelId = 1
            },
            new Review
            {
                ReviewId = 2,
                Comment = "Bad Service...",
                Rating = 2,
                CustomerId = 2,
                HotelId = 1
            }
        );
    }
}
