using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.Data.Models
{
    public class VerifyEmailDTO
    {
        public string Email { get; set; }
        public string OTP { get; set; }
    }
}