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
using EvaluationAPI.BLL;
using EvaluationAPI.DAL;
using EvaluationAPI.BLL.Constants;
using EvaluationAPI.Models.Settings;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EvaluationAPI.BLL.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.Responses;
using EvaluationAPI.Presenters;
using FluentValidation.AspNetCore;
using FluentValidation;
using EvaluationAPI.BLL.Requests;
using EvaluationAPI.Models.Validators;

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
            services.AddBLLServices();
            services.AddDALServices(Configuration.GetConnectionString("DataDatabase"));

            services.AddIdentity<EvaluationUser, IdentityRole>()
             .AddEntityFrameworkStores<EvIdentityContext>()
             .AddDefaultTokenProviders();

            var authSettings = Configuration.GetSection(nameof(AuthSettings));
            services.Configure<AuthSettings>(authSettings);

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authSettings[nameof(AuthSettings.SecretKey)]));

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;

                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
                options.AddPolicy("TestEditor", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.TestA, Constants.Strings.JwtClaims.TestAccess));
                options.AddPolicy("UserEditor", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.UserA, Constants.Strings.JwtClaims.UserAccess));
             });

            var identityBuilder = services.AddIdentityCore<EvaluationUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<EvIdentityContext>().AddDefaultTokenProviders();



            services.AddMvc().AddFluentValidation().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
           
            services.AddTransient<IValidator<LoginRequest>, LoginRequestValidator>();
            services.AddTransient<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>();
            

             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "AspNetCoreApiStarter", Version = "v1" });
                // Swagger 2.+ support
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
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
            app.Run(context => {
                context.Response.Redirect("../swagger");
                return Task.CompletedTask;
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
