using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentWeb.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string deanEmail = "dean@gmail.com";
            string password = "12345w!";
            if (await roleManager.FindByNameAsync("teacher") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("teacher"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (await roleManager.FindByNameAsync("dean") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("dean"));
            }
            if (await userManager.FindByNameAsync(deanEmail) == null)
            {
                ApplicationUser dean = new ApplicationUser { Email = deanEmail, UserName = deanEmail, Name = "dean", Surname = "naed" };
                IdentityResult result = await userManager.CreateAsync(dean, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(dean, "dean");
                }
            }
        }
    }
}
