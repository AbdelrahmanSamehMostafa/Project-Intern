using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models
{
    public class CustomerDTO
    {

        public int CustomerId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<String> Wishlists { get; set; }
    }
}