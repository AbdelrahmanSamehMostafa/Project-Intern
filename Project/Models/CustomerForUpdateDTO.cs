using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models
{
    public record CustomerForUpdateDTO
    {

        public string Name { get; set; }

        public string Email { get; set; }

    }
}