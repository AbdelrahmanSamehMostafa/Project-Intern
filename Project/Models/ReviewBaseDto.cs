namespace HotelBookingSystem.Models
{
    public record ReviewBaseDto
    {
        public string Comment { get; set; }
        
        public int Rating { get; set; }
    }
    
}