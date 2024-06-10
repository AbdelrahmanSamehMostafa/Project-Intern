namespace HotelBookingSystem.Models
{
    public class RoomDTO
    {
        //public RoomType RoomType { get; set; }
        
        public string RoomType { get; set; }
        
        public string Description { get; set; }
        public double Price { get; set; }
        public bool isAvailable { get; set; }
    }
    
}