using Back_end_Project;
using Back_end_Project.context;
using Back_end_Project.Helpers;
using Back_end_Project.Helpers.EmailService.EmailSender;
using Back_end_Project.Helpers.EmailService.EmailSender.Abstract;
using Back_end_Project.Helpers.EmailService.EmailSender.Concrete;
using Back_end_Project.Models;
//using Back_end_Project.ServiceRegistrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddHttpContextAccessor();
//builder.Services.Register(builder.Configuration);
builder.Services.AddDbContext<EHDbContext>(opt => {


    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredLength = 8;

    opt.User.RequireUniqueEmail = true;
    opt.SignIn.RequireConfirmedEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
})
    .AddEntityFrameworkStores<EHDbContext>()
    
	.AddDefaultTokenProviders();

var configuration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<IEmailSender, EmailSender>();

//builder.Services.ConfigureApplicationCookie(options =>
//{
//	options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
//	{
//		if (context.HttpContext.Request.Path.Value.StartsWith("/admin") || context.HttpContext.Request.Path.Value.StartsWith("/Admin"))
//		{
//			var redirectPath = new Uri(context.RedirectUri);
//			context.Response.Redirect("/admin/account/login" + redirectPath.Query);
//		}
//		else
//		{
//			var redirectPath = new Uri(context.RedirectUri);
//			context.Response.Redirect("/account/login" + redirectPath.Query);
//		}
//		return Task.CompletedTask;
//	};
//});




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=account}/{action=login}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseAuthentication();
app.UseAuthorization();

//var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
//using(var scope = scopeFactory.CreateScope())
//{
//    var userManager = scope.ServiceProvider.GetService<UserManager<User>>();
//    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
//    await DbInitializer.SeedAsync(userManager, roleManager);
//}

app.Run();
