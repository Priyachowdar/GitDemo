using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventcore.Models
{
    public class DataSeeder
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public DataSeeder(UserManager<IdentityUser> userManager,
                          RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
            if (!await roleManager.RoleExistsAsync("Employees"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Employees" });
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        }
        public async Task SeedAdmin()
        {
            var obj = await userManager.FindByNameAsync("admin");
            if (obj == null)
            {
                var admin = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@gmail.com",
                    PhoneNumber = "1232312123"
                };
                await userManager.CreateAsync(admin, "Admin@123");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
