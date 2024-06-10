using Microsoft.EntityFrameworkCore;

public static class BookingSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>().HasData(
            new Booking
            {
                BookingId = 1,
                CustomerId = 1,
                RoomId = 1,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            }
        );
    }
}
