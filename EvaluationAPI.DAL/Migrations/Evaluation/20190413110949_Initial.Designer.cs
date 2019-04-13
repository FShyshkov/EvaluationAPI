﻿// <auto-generated />
using EvaluationAPI.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EvaluationAPI.DAL.Migrations.Evaluation
{
    [DbContext(typeof(EvaluationContext))]
    [Migration("20190413110949_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EvaluationAPI.DAL.Entities.Question", b =>
                {
                    b.Property<int>("QuestionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("varchar(max)");

                    b.Property<int>("TestId");

                    b.HasKey("QuestionId");

                    b.HasIndex("TestId");

                    b.ToTable("Questions","TestsDB");

                    b.HasData(
                        new
                        {
                            QuestionId = 1,
                            Answer = "0100",
                            Name = "C# class can inherit multiple ________",
                            QuestionText = "Class#-#Interface#-#Abstract class#-#Static class",
                            TestId = 1
                        },
                        new
                        {
                            QuestionId = 2,
                            Answer = "0001",
                            Name = "Which of the followings are value types in C#?",
                            QuestionText = "Int32#-#Double#-#Decimal#-#All of the above",
                            TestId = 1
                        },
                        new
                        {
                            QuestionId = 3,
                            Answer = "1000",
                            Name = "Which of the following is a reference type in C#?",
                            QuestionText = "String#-#Long#-#Boolean#-#None of the above",
                            TestId = 1
                        },
                        new
                        {
                            QuestionId = 4,
                            Answer = "010",
                            Name = "What is Nullable type??",
                            QuestionText = "It allows assignment of null to reference type.#-#It allows assignment of null to value type.#-#It allows assignment of null to static class.",
                            TestId = 1
                        },
                        new
                        {
                            QuestionId = 5,
                            Answer = "010",
                            Name = "Struct is a _____.",
                            QuestionText = "Reference type#-#Value type#-#Class type",
                            TestId = 1
                        },
                        new
                        {
                            QuestionId = 6,
                            Answer = "10010",
                            Name = "Pick all correct ways to initialize a two-dimensional array",
                            QuestionText = "int[,] k = {{2,-2},{3,-2},{0,4}}#-#int k[,] = new int[2,3]#-#int[][] k = new int[2][]#-#int[,] k = new int[2,3]#-#int k[][] = new int[2][3]",
                            TestId = 2
                        },
                        new
                        {
                            QuestionId = 7,
                            Answer = "1110",
                            Name = "Which of the following is true about C# structures?",
                            QuestionText = "Unlike classes, structures cannot inherit other structures or classes.#-#Structure members cannot be specified as abstract, virtual, or protected.#-#A structure can implement one or more interfaces.#-#Structure is a reference type",
                            TestId = 2
                        },
                        new
                        {
                            QuestionId = 8,
                            Answer = "1010",
                            Name = "Which of the following access specifiers in C# allow the members to be inherited?",
                            QuestionText = "public#-#private#-#protected#-#internal",
                            TestId = 2
                        });
                });

            modelBuilder.Entity("EvaluationAPI.DAL.Entities.Result", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TestId");

                    b.Property<int>("TestResult")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("ResultId");

                    b.HasIndex("TestId");

                    b.ToTable("Results","TestsDB");

                    b.HasData(
                        new
                        {
                            ResultId = 1,
                            TestId = 1,
                            TestResult = 80,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 2,
                            TestId = 1,
                            TestResult = 70,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 3,
                            TestId = 1,
                            TestResult = 60,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 4,
                            TestId = 1,
                            TestResult = 50,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 5,
                            TestId = 1,
                            TestResult = 40,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 6,
                            TestId = 1,
                            TestResult = 30,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 7,
                            TestId = 1,
                            TestResult = 20,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 8,
                            TestId = 1,
                            TestResult = 10,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 9,
                            TestId = 1,
                            TestResult = 90,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 10,
                            TestId = 1,
                            TestResult = 10,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 11,
                            TestId = 2,
                            TestResult = 90,
                            UserName = "TestUser"
                        },
                        new
                        {
                            ResultId = 12,
                            TestId = 2,
                            TestResult = 20,
                            UserName = "TestUser"
                        });
                });

            modelBuilder.Entity("EvaluationAPI.DAL.Entities.Test", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("TestName")
                        .IsRequired()
                        .HasColumnType("varchar(25)");

                    b.HasKey("TestId");

                    b.ToTable("Tests","TestsDB");

                    b.HasData(
                        new
                        {
                            TestId = 1,
                            TestName = "C#Basics"
                        },
                        new
                        {
                            TestId = 2,
                            TestName = "C#MChoice"
                        });
                });

            modelBuilder.Entity("EvaluationAPI.DAL.Entities.Question", b =>
                {
                    b.HasOne("EvaluationAPI.DAL.Entities.Test", "Test")
                        .WithMany("Questions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EvaluationAPI.DAL.Entities.Result", b =>
                {
                    b.HasOne("EvaluationAPI.DAL.Entities.Test", "Test")
                        .WithMany("Results")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}