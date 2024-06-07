namespace HotelBookingSystem.Models
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }
        public int TotalNumberOfRooms { get; set; }
        public int NumberOfAvailableRooms { get; set; }
        public string Description { get; set; }
        public string ContactInfo { get; set; }
        public Address Address { get; set; }
        public List<string> Entertainments { get; set; }
        public List<string> ImageUrls { get; set; }
        // Other relevant properties...

        // Optionally, you might include additional calculated properties or methods here
    }
}