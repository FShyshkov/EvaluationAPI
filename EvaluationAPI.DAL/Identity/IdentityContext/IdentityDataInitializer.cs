using EvaluationAPI.DAL.Identity.IdentityEntity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EvaluationAPI.DAL.Identity.IdentityContext
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<EvaluationUser>>();
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<EvaluationUser> userManager)
        {
            if (userManager.FindByNameAsync("TestModerator").Result == null)
            {
                EvaluationUser user = new EvaluationUser();
                user.UserName = "TestModerator";
                user.Email = "moder@cg.com";
                user.FirstName = "Epic";
                user.LastName = "Moderator";

                IdentityResult result = userManager.CreateAsync
                (user, "Moderator123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Moderator").Wait();
                }
            }


            if (userManager.FindByNameAsync("TestAdmin").Result == null)
            {
                EvaluationUser user = new EvaluationUser();
                user.UserName = "TestAdmin";
                user.Email = "ad@lhost.ua";
                user.FirstName = "Epic";
                user.LastName = "Admin";

                IdentityResult result = userManager.CreateAsync
                (user, "Admin123").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("ApiUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "ApiUser";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Moderator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Moderator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}

