using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EvaluationAPI.DAL.Identity.IdentityContext;
using EvaluationAPI.DAL.Identity.IdentityEntity;
using EvaluationAPI.DAL.Context;
using EvaluationAPI.DAL.UOW;
using EvaluationAPI.DAL.Contracts;
using EvaluationAPI.DAL.Identity;
using EvaluationAPI.DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace EvaluationAPI.DAL
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDALServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<EvIdentityContext>(options =>
              options.UseSqlServer(connectionString,
              b => b.MigrationsAssembly("EvaluationAPI.DAL")));

            services.AddDbContext<EvaluationContext>(
                options => options.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("EvaluationAPI.DAL")));
            
            services.AddTransient<IUnitOfWork, EvaluationUOW>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<IResultRepository, ResultRepository>();
        }
    }
}
