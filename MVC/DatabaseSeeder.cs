using Microsoft.AspNetCore.Identity;

namespace MVC
{
    public static class DatabaseSeeder
    {
        public static async Task SeedDataAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = ["Admin", "User"];
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string userName = "admin";
            string email = "admin@gmail.com";

            if (await userManager.FindByEmailAsync(email) is null)
            {
                IdentityUser admin = new IdentityUser
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true,
                };
                IdentityResult result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
