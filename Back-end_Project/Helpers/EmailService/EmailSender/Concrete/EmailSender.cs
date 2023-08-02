using Back_end_Project.Helpers.EmailService.EmailSender.Abstract;
using MailKit.Net.Smtp;
using MimeKit;

namespace Back_end_Project.Helpers.EmailService.EmailSender.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("FJeikhun", _configuration.UserName));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
					client.AuthenticationMechanisms.Remove("XOAUTH2");

                    //client.Connect(_configuration.SmtpServer, _configuration.Port, true);
					//client.Authenticate(_configuration.UserName, _configuration.Password);
					client.Connect("smtp.gmail.com", 465, true);
					client.Authenticate("jeikhun.jalil@gmail.com", "Ceyhun2002");
                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}
