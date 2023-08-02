//using Back_end_Project.context;
//using Back_end_Project.Models;
//using Back_end_Project.Services.Implementations;
//using Back_end_Project.Services.Interfaces;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Back_end_Project.ServiceRegistrations
//{
//    public static class ServiceRegister
//    {
//        public static void Register(this IServiceCollection service, ConfigurationManager configuration)
//        {
//            service.AddScoped<IMailService, MailService>();
//            service.AddIdentity<User, IdentityRole>()
//                   .AddDefaultTokenProviders()
//                   .AddEntityFrameworkStores<EHDbContext>();
//            service.Configure<IdentityOptions>(options =>
//            {
//                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//                options.Lockout.MaxFailedAccessAttempts = 3;
//                options.Lockout.AllowedForNewUsers = true;
//                options.Password.RequireDigit = true;
//                options.Password.RequiredLength = 8;
//                options.User.RequireUniqueEmail = true;
//                options.SignIn.RequireConfirmedEmail = true;
//            });
//            service.AddDbContext<EHDbContext>(opt =>
//            {
//                opt.UseSqlServer(configuration.GetConnectionString("Default"));
//            });
//        }
//    }
//}
