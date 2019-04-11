using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using EvaluationAPI.BLL.Contracts;
using EvaluationAPI.BLL.Services;

namespace EvaluationAPI.BLL
{
    public static class ServiceCollectionExtensions
    {
            public static void AddBLLServices(this IServiceCollection services)
            {
                services.AddTransient<IUserService, UserService>();

            services.AddTransient<IJwtFactory, JwtFactory>();
            services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();

        }
    }
   
}
