namespace HotelBookingSystem.Models
{
    public record RoomDTO
    {
        public string RoomType { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool isBooked { get; set; }
        public bool isAvailable { get; set; }
    }
    
}