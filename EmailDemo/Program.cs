using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;

namespace EmailDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sender = new SmtpSender(() => new SmtpClient("localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25 // Check on Papercut Smtp App
                //DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                //PickupDirectoryLocation = @"C:\Demos"
            });

            StringBuilder template = new StringBuilder();
            template.AppendLine("Dear @Model.FirstName,");
            template.AppendLine("<p>Thanks for purchasing @Model.ProductName. We hope you enjoy it!</p>");
            template.AppendLine("- The Filipe Silva Team");

            Email.DefaultSender = sender;
            Email.DefaultRenderer = new RazorRenderer();

            var email = await Email
                .From("filipe@email.com")
                .To("test@test.com", "Sue")
                .Subject("Thanks!")
                .UsingTemplate(template.ToString(), new { FirstName = "Filipe", ProductName = "Bacon-Wrapped Bacon" })
                //.Body("Thanks for buying our product!")
                .SendAsync();
        }
    }
}
