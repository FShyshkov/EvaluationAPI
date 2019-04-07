
using EvaluationAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationAPI.DAL.Context.Configurations
{
    public class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            // Mapping for table
            builder.ToTable("Results", "TestsDB");
            // Primary key
            builder.HasKey(p => p.ResultId);
            // Generate property on Add
            builder.Property(p => p.ResultId).ValueGeneratedOnAdd();
            // Set mapping for columns
            builder.Property(p => p.TestResult).HasColumnType("int").IsRequired();
            builder.Property(p => p.UserName).HasColumnType("varchar(25)").IsRequired();
            //Set foreign key
            builder.HasOne(p => p.Test).WithMany(d => d.Results).HasForeignKey(p => p.TestId);
        }
    }
}
