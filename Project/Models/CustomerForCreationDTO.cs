using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models
{
    public record CustomerForCreationDTO : UserForCreationDTO
    {
        public List<String> Wishlists { get; set; }
    }
}