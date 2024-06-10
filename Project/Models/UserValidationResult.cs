namespace HotelBookingSystem.Models
{
    public record UserValidationResult
    {
        public dynamic User { get; set; }
        public string Role { get; set; }
    }
}