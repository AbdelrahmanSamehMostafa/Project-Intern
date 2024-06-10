namespace HotelBookingSystem.Models
{
    public record ReviewWithIdDTO : ReviewDTO
    {

        public int ReviewId { get; set; }
        //public DateTime Date { get; set; } = DateTime.Now;


    }
    
}