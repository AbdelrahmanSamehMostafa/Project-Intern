using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public record HotelUpdateDto
    {

        public string Name { get; set; }
        public AddressBaseDTO Address { get; set; }
        public string Description { get; set; }
        public List<string> Entertainments { get; set; }
        public List<string>? ImageUrls { get; set; }
    }
}
