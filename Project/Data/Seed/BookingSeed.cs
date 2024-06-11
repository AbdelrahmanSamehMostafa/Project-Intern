using Microsoft.EntityFrameworkCore;
using System;

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
            },
            new Booking
            {
                BookingId = 2,
                CustomerId = 2,
                RoomId = 2,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 3,
                CustomerId = 1,
                RoomId = 3,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 4,
                CustomerId = 3,
                RoomId = 4,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 5,
                CustomerId = 2,
                RoomId = 5,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 6,
                CustomerId = 3,
                RoomId = 6,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 7,
                CustomerId = 4,
                RoomId = 7,
                CheckInDate = DateTime.Now,
                CheckOutDate = DateTime.Now.AddDays(2),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 8,
                CustomerId = 1,
                RoomId = 8,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(5),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 9,
                CustomerId = 2,
                RoomId = 9,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(5),
                Status = "Confirmed"
            },
            new Booking
            {
                BookingId = 10,
                CustomerId = 3,
                RoomId = 10,
                CheckInDate = DateTime.Now.AddDays(3),
                CheckOutDate = DateTime.Now.AddDays(5),
                Status = "Confirmed"
            }
        );
    }
}
