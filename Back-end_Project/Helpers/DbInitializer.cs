using Back_end_Project.Constants;
using Back_end_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Back_end_Project.Helpers
{
    public static class DbInitializer
    {
        public async static Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await roleManager.RoleExistsAsync(role.ToString()))
                {
                    await roleManager.CreateAsync(new IdentityRole
                    {
                        Name=role.ToString(),
                    });

                }
            }
            if ((await userManager.FindByNameAsync("admin")) == null)
            {

                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@mail.ru",
                    EmailConfirmed = true,
                };
                var result = await userManager.CreateAsync(user, "Admin123!");

                if (!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        throw new Exception(error.Description);
                    }
                }
                    await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }

        }
    }
}
