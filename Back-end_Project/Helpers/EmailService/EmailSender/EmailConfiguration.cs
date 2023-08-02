namespace Back_end_Project.Helpers.EmailService.EmailSender
{
    public class EmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
