using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using RazorEngine;

namespace Hsking.Api.Dao.Emails.Mailers
{ 
    public class UserMailer :  IUserMailer 	
	{
		public UserMailer()
		{
			
		}
		
        public Task ConfirmEmail(string email,string code)
        {
             string template =
              @"<html>
                  <head>
                    <title>Hello @Model.Email</title>
                  </head>
                  <body>
                    Email: @Model.Url
                  </body>
                </html>";
           
          var result=Razor.Parse(template, new {Email = email, Url = code});
          return SendEmail("dimaivanov1@mail.ru", email, "Valid Acc", result, true);
        }

        public Task RecoveryPasswordEmail(string email, string code)
        {
            string template =
             @"<html>
                  <head>
                    <title>Hello @Model.Email</title>
                  </head>
                  <body>
                    Email: @Model.Url
                  </body>
                </html>";

            var result = Razor.Parse(template, new { Email = email, Url = code });
            return SendEmail("dimaivanov1@mail.ru", email, "Valid Acc", result, true);
        }

        private  Task SendEmail(string from, string to, string subject, string body, bool isHtml)
        {
            SmtpClient mailClient = new SmtpClient("smtp.mail.ru", 25);
            mailClient.Credentials = new NetworkCredential("dimaivanov1@mail.ru", "dimarianon1990");
            mailClient.EnableSsl = true;

            MailMessage message = new MailMessage();
            message.From = new MailAddress(from);
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;
        
            return mailClient.SendMailAsync(message);
        }
    }

  
}