using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EvaluationAPI.DAL.Identity.IdentityContext;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.DAL.Context;
using Swashbuckle.AspNetCore.Swagger;

namespace EvaluationAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EvIdentityContext>(  options =>
                options.UseSqlServer(Configuration.GetConnectionString("DataDatabase"),
                b => b.MigrationsAssembly("EvaluationAPI.DAL")));

            services.AddDbContext<EvaluationContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DataDatabase"), 
                b => b.MigrationsAssembly("EvaluationAPI.DAL")));

            var identityBuilder = services.AddIdentityCore<EvaluationUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });

            services.AddIdentity<EvaluationUser, IdentityRole>()
              .AddEntityFrameworkStores<EvIdentityContext>()
              .AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

           

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Evaluation API",
                    Description = "ASP.NET Core knowledge evaluation API"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            CreateRoles(serviceProvider).Wait();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Evaluation API V1");
            });
        }
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles   
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<EvaluationUser>>();
            string[] roleNames = { "Admin", "User", "Moderator" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1  
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            EvaluationUser user = await UserManager.FindByEmailAsync("admin@goomail.com");

            if (user == null)
            {
                user = new EvaluationUser()
                {
                    UserName = "TestAdmin",
                    Email = "admin@goomail.com",
                };
                await UserManager.CreateAsync(user, "Admin123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");


            EvaluationUser user1 = await UserManager.FindByEmailAsync("test@il.com");

            if (user1 == null)
            {
                user1 = new EvaluationUser()
                {
                    UserName = "TestUser",
                    Email = "test@il.com",
                };
                await UserManager.CreateAsync(user1, "User123");
            }
            await UserManager.AddToRoleAsync(user1, "User");

            EvaluationUser user2 = await UserManager.FindByEmailAsync("moder@goomail.com");

            if (user2 == null)
            {
                user2 = new EvaluationUser()
                {
                    UserName = "TestModer",
                    Email = "moder@goomail.com",
                };
                await UserManager.CreateAsync(user2, "Moder123");
            }
            await UserManager.AddToRoleAsync(user2, "Moderator");

        } 
    }
}
