
using EvaluationAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationAPI.DAL.Context.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            // Mapping for table
            builder.ToTable("Questions", "TestsDB");
            // Primary key
            builder.HasKey(p => p.QuestionId);
            // Generate property on Add
            builder.Property(p => p.QuestionId).ValueGeneratedOnAdd();
            // Set mapping for columns
            builder.Property(p => p.Name).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.QuestionText).HasColumnType("varchar(max)").IsRequired();
            builder.Property(p => p.Answer).HasColumnType("varchar(25)").IsRequired();
            //Set foreign key
            builder.HasOne(p => p.Test).WithMany(p => p.Questions).HasForeignKey(p => p.TestId);
        }
    }
}
