using Microsoft.EntityFrameworkCore;
using EvaluationAPI.DAL.Entities;
using EvaluationAPI.DAL.Context.Configurations;

namespace EvaluationAPI.DAL.Context
{
    public class EvaluationContext : DbContext
    {
        public EvaluationContext(DbContextOptions<EvaluationContext> options)
            : base(options)
        {
        }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionConfiguration())
                .ApplyConfiguration(new ResultConfiguration())
                .ApplyConfiguration(new TestConfiguration());
        }
    }
}
