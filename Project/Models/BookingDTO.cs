namespace HotelBookingSystem.Models{
public record BookingDTO:BookingBaseDTO
{
    public int CustomerId { get; set; }
    public int RoomId { get; set; }

}
}
