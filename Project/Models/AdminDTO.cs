using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Models
{
    public class AdminDTO
    {
        public int AdminId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //public bool IsActive { get; set; } = false;

        //public ICollection<Hotel>? Hotels { get; set; }
    }
}