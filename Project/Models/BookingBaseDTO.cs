namespace HotelBookingSystem.Models{
public record BookingBaseDTO
{
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string? Status { get; set; }

     
}
}
