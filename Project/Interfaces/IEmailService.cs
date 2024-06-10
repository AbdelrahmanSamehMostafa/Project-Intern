using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingSystem.interfaces
{
    public interface IEmailService
    {

        Task SendEmailAsync(string email, string subject, string message);


    }
}