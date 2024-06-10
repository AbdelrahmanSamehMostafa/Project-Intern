using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingSystem.Models
{
    public class HotelUpdateDto : HotelBaseDto
    {
        
        public override string Name { get; set; }

        public override string? ContactInfo { get; set; }
        public double Rating { get; set; }
    }
}
