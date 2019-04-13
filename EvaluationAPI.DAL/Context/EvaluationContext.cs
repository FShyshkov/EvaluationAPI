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
                new Test { TestId = 1, TestName = "C#Basics" },
                new Test { TestId = 2, TestName = "C#MChoice" });
            #endregion

            #region QuestionSeed
            modelBuilder.Entity<Question>().HasData(
                new Question { QuestionId = 1, Name = "C# class can inherit multiple ________", QuestionText = "Class#-#Interface#-#Abstract class#-#Static class", TestId = 1, Answer = "0100" },
                new Question { QuestionId = 2, Name = "Which of the followings are value types in C#?", QuestionText = "Int32#-#Double#-#Decimal#-#All of the above", TestId = 1, Answer = "0001" },
                new Question { QuestionId = 3, Name = "Which of the following is a reference type in C#?", QuestionText = "String#-#Long#-#Boolean#-#None of the above", TestId = 1, Answer = "1000" },
                new Question { QuestionId = 4, Name = "What is Nullable type??", QuestionText = "It allows assignment of null to reference type.#-#It allows assignment of null to value type.#-#It allows assignment of null to static class.", TestId = 1, Answer = "010" },
                new Question { QuestionId = 5, Name = "Struct is a _____.", QuestionText = "Reference type#-#Value type#-#Class type", TestId = 1, Answer = "010" },
                new Question { QuestionId = 6, Name = "Pick all correct ways to initialize a two-dimensional array", QuestionText = "int[,] k = {{2,-2},{3,-2},{0,4}}#-#int k[,] = new int[2,3]#-#int[][] k = new int[2][]#-#int[,] k = new int[2,3]#-#int k[][] = new int[2][3]", TestId = 2, Answer = "10010" },
                new Question { QuestionId = 7, Name = "Which of the following is true about C# structures?", QuestionText = "Unlike classes, structures cannot inherit other structures or classes.#-#Structure members cannot be specified as abstract, virtual, or protected.#-#A structure can implement one or more interfaces.#-#Structure is a reference type", TestId = 2, Answer = "1110" },
                new Question { QuestionId = 8, Name = "Which of the following access specifiers in C# allow the members to be inherited?", QuestionText = "public#-#private#-#protected#-#internal", TestId = 2, Answer = "1010" });
            #endregion

            #region ResultSeed
            modelBuilder.Entity<Result>().HasData(
                new Result { ResultId = 1,UserName = "TestUser", TestId = 1, TestResult = 80 },
                new Result { ResultId = 2, UserName = "TestUser", TestId = 1, TestResult = 70 },
                new Result { ResultId = 3, UserName = "TestUser", TestId = 1, TestResult = 60 },
                new Result { ResultId = 4, UserName = "TestUser", TestId = 1, TestResult = 50 },
                new Result { ResultId = 5, UserName = "TestUser", TestId = 1, TestResult = 40 },
                new Result { ResultId = 6, UserName = "TestUser", TestId = 1, TestResult = 30 },
                new Result { ResultId = 7, UserName = "TestUser", TestId = 1, TestResult = 20 },
                new Result { ResultId = 8, UserName = "TestUser", TestId = 1, TestResult = 10 },
                new Result { ResultId = 9, UserName = "TestUser", TestId = 1, TestResult = 90 },
                new Result { ResultId = 10, UserName = "TestUser", TestId = 1, TestResult = 10 },

                new Result { ResultId = 11, UserName = "TestUser", TestId = 2, TestResult = 90 },
                new Result { ResultId = 12, UserName = "TestUser", TestId = 2, TestResult = 20 }
                );
            #endregion
        }
    }
}
