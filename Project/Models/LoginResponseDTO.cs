using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models
{
    public record LoginResponseDTO
    {
        public string Token { get; set; }
    }

}