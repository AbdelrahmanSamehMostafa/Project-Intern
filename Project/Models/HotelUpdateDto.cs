using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class HotelUpdateDto
    {
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Total number of rooms must be at least 1")]
        public int? TotalNumberOfRooms { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of available rooms cannot be negative")]
        public int? NumberOfAvailableRooms { get; set; }

        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Contact information can't be longer than 100 characters.")]
        [RegularExpression(@"(^\+[0-9]{1,3})?(\([0-9]{1,4}\))?[-. \\\/]?(\(?[0-9]+\)?[-. \\\/\\/]?)+$", ErrorMessage = "Invalid contact info format.")]
        public string? ContactInfo { get; set; }
        public List<string>? Entertainments { get; set; }

        public List<string>? ImageUrls { get; set; }
        public double averageRating { get; set; }
        public double Rating { get; set; }
    }
}
