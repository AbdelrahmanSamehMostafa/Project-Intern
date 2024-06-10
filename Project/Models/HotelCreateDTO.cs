using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelBookingSystem.Models;


namespace HotelBookingSystem.Models
{
    public class HotelCreateDto : HotelBaseDto
    {
        [Required]
        public override string Name { get; set; }

        [Required]
        public int AdminId { get; set; }

        [Required]
        public override string ContactInfo { get; set; }

        [Required]
        public AddressBaseDTO Address { get; set; }

    }


}