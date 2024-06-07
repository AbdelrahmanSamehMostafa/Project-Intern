namespace HotelBookingSystem.Models
{
    public class RoomDTO
    {
        public string RoomType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public bool Availability { get; set; }
    }
    
}