using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HotelBookingSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _sendGridApiKey;

        public EmailService(string sendGridApiKey)
        {
            _sendGridApiKey = sendGridApiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_sendGridApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("abdelrahmansmostafa@gmail.com", "Hotel Booking System"), // Replace with your verified sender email
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            try
            {
                var response = await client.SendEmailAsync(msg);

                // Check response status code
                if (response.StatusCode != System.Net.HttpStatusCode.OK && response.StatusCode != System.Net.HttpStatusCode.Accepted)
                {
                    var responseBody = await response.Body.ReadAsStringAsync();
                    throw new ApplicationException($"Failed to send email. Status code: {response.StatusCode}. Response body: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Exception while sending email: {ex.Message}");
                throw; // Re-throw the exception to propagate it
            }
        }
    }

}