using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistance.Configuration
{
    public class Seed
    {
        public static async void SeedUsers(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Employee>>();
                var admin = userManager.FindByEmailAsync("ma7moud.asp@gmail.com").Result;
                if (admin == null)
                {
                    var res = await userManager.CreateAsync(new Employee
                    {
                        Email = "ma7moud.asp@gmail.com",
                        UserName = "Mahmoud",
                        FullName = "Mahmoud Nasr"
                    }, "Aa11111111");
                    if (res.Succeeded)
                    {
                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        var adminRole = roleManager.Roles.FirstOrDefault(u => u.Name == Roles.Administrator.ToString().ToLower());
                        var adminUser = userManager.Users.FirstOrDefault(u => u.Email == "ma7moud.asp@gmail.com");
                        await userManager.AddToRoleAsync(adminUser, adminRole.Name);
                    }
                }
                var emp = userManager.Users.FirstOrDefault(u => u.Email == "ahmed.ali@gmail.com");
                if (emp == null)
                {
                    var res = await userManager.CreateAsync(new Employee
                    {
                        Email = "ahmed.ali@gmail.com",
                        UserName = "Ahmed",
                        FullName = "Ahmed Ali"
                    }, "Aa11111111");
                    if (res.Succeeded)
                    {
                        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                        var adminRole = roleManager.Roles.FirstOrDefault(u => u.Name == Roles.Administrator.ToString().ToLower());
                        var empUser = userManager.Users.FirstOrDefault(u => u.Email == "ahmed.ali@gmail.com");
                        var empRole = roleManager.Roles.FirstOrDefault(u => u.Name == Roles.Employee.ToString().ToLower());
                        await userManager.AddToRoleAsync(empUser, empRole.Name);
                    }
                }
            }
        }
    }
}
