namespace Back_end_Project.Helpers.EmailService.EmailSender.Abstract
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
