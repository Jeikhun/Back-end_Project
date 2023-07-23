
using Back_end_Project.EmailServices;

namespace Back_end_Project
{
    public static class Startup
    {

        public static void Register(this IServiceCollection service, IConfiguration _configuration)
        {
            

            //service.AddScoped<IMailService , SmtpEmailSender>(i=> 
            //    new SmtpEmailSender(
            //        _configuration["EmailSender:Host"],
            //        _configuration.GetValue<int>("EmailSender:Port"),
            //        _configuration.GetValue<bool>("EmailSender:EnableSSL"),
            //        _configuration["EmailSender:UserName"],
            //        _configuration["EmailSender:Password"])
            //    );

        }
    }
}
