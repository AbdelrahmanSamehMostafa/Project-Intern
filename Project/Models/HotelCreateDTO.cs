using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelBookingSystem.Models;


namespace HotelBookingSystem.Models
{
    public record HotelCreateDto : HotelBaseDto
    {


        [Required]
        public int AdminId { get; set; }

        [Required]
        public override string ContactInfo { get; set; }

        [Required]
        public AddressBaseDTO Address { get; set; }

    }


}