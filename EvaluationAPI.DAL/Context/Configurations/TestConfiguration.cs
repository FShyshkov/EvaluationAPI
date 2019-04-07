
using EvaluationAPI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaluationAPI.DAL.Context.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            // Mapping for table
            builder.ToTable("Tests", "TestsDB");
            // Primary key
            builder.HasKey(p => p.TestId);
            // Generate property on Add
            builder.Property(p => p.TestId).ValueGeneratedOnAdd();
            // Set mapping for columns
            builder.Property(p => p.TestName).HasColumnType("varchar(25)").IsRequired();
            //Set foreign key
            builder.HasMany(p => p.Questions).WithOne(d => d.Test);
            builder.HasMany(p => p.Results).WithOne(d => d.Test);
        }
    }
}
