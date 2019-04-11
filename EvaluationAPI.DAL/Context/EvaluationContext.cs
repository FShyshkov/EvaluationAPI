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

            #region TestSeed
            modelBuilder.Entity<Test>().HasData(
                new Test { TestId = 1, TestName = "Test1"},
                new Test { TestId = 2, TestName = "Test2" },
                new Test { TestId = 3, TestName = "Test3" });
            #endregion

            #region QuestionSeed
            modelBuilder.Entity<Question>().HasData(
                new Question { QuestionId = 1,  Name = "Question1?", QuestionText = "PossibleAnswer1&CorrectAnswer&PossibleAnswer3", TestId = 1, Answer = "010"},
                new Question { QuestionId = 2, Name = "Question2?", QuestionText = "PossibleAnswer1 asd&PossibleAnswer 2& CorrectAnswer& Answer4", TestId = 1, Answer ="0010"},
                new Question { QuestionId = 3, Name = "Question3?", QuestionText = "PossibleAnswer21 asd&Correct Answer&Possib4le  234252Answer3& Answer4", TestId = 1, Answer = "0100" },
                new Question { QuestionId = 4, Name = "Question4?", QuestionText = "PossibleAnswe4r1 asd&Correct answer&Possible252Answer3", TestId = 2, Answer = "010" },
                new Question { QuestionId = 5, Name = "Question5?", QuestionText = "CorrectAnswer1&Possinswer 2&CorrectAnswer2& PossibleAnswer4& PossibleAnswer5", TestId = 2, Answer = "10100" });
            #endregion

            #region ResultSeed
            #endregion
        }
    }
}
