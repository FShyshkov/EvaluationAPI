using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationAPI.DAL.Migrations.Evaluation
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TestsDB");

            migrationBuilder.CreateTable(
                name: "Tests",
                schema: "TestsDB",
                columns: table => new
                {
                    TestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TestName = table.Column<string>(type: "varchar(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.TestId);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                schema: "TestsDB",
                columns: table => new
                {
                    QuestionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(max)", nullable: false),
                    QuestionText = table.Column<string>(type: "varchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "varchar(25)", nullable: false),
                    TestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalSchema: "TestsDB",
                        principalTable: "Tests",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                schema: "TestsDB",
                columns: table => new
                {
                    ResultId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "varchar(25)", nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    TestResult = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ResultId);
                    table.ForeignKey(
                        name: "FK_Results_Tests_TestId",
                        column: x => x.TestId,
                        principalSchema: "TestsDB",
                        principalTable: "Tests",
                        principalColumn: "TestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "TestsDB",
                table: "Tests",
                columns: new[] { "TestId", "TestName" },
                values: new object[] { 1, "C#Basics" });

            migrationBuilder.InsertData(
                schema: "TestsDB",
                table: "Tests",
                columns: new[] { "TestId", "TestName" },
                values: new object[] { 2, "C#MChoice" });

            migrationBuilder.InsertData(
                schema: "TestsDB",
                table: "Questions",
                columns: new[] { "QuestionId", "Answer", "Name", "QuestionText", "TestId" },
                values: new object[,]
                {
                    { 1, "0100", "C# class can inherit multiple ________", "Class#-#Interface#-#Abstract class#-#Static class", 1 },
                    { 2, "0001", "Which of the followings are value types in C#?", "Int32#-#Double#-#Decimal#-#All of the above", 1 },
                    { 3, "1000", "Which of the following is a reference type in C#?", "String#-#Long#-#Boolean#-#None of the above", 1 },
                    { 4, "010", "What is Nullable type??", "It allows assignment of null to reference type.#-#It allows assignment of null to value type.#-#It allows assignment of null to static class.", 1 },
                    { 5, "010", "Struct is a _____.", "Reference type#-#Value type#-#Class type", 1 },
                    { 8, "1010", "Which of the following access specifiers in C# allow the members to be inherited?", "public#-#private#-#protected#-#internal", 2 },
                    { 7, "1110", "Which of the following is true about C# structures?", "Unlike classes, structures cannot inherit other structures or classes.#-#Structure members cannot be specified as abstract, virtual, or protected.#-#A structure can implement one or more interfaces.#-#Structure is a reference type", 2 },
                    { 6, "10010", "Pick all correct ways to initialize a two-dimensional array", "int[,] k = {{2,-2},{3,-2},{0,4}}#-#int k[,] = new int[2,3]#-#int[][] k = new int[2][]#-#int[,] k = new int[2,3]#-#int k[][] = new int[2][3]", 2 }
                });

            migrationBuilder.InsertData(
                schema: "TestsDB",
                table: "Results",
                columns: new[] { "ResultId", "TestId", "TestResult", "UserName" },
                values: new object[,]
                {
                    { 10, 1, 10, "TestUser" },
                    { 9, 1, 90, "TestUser" },
                    { 8, 1, 10, "TestUser" },
                    { 5, 1, 40, "TestUser" },
                    { 6, 1, 30, "TestUser" },
                    { 11, 2, 90, "TestUser" },
                    { 4, 1, 50, "TestUser" },
                    { 3, 1, 60, "TestUser" },
                    { 2, 1, 70, "TestUser" },
                    { 1, 1, 80, "TestUser" },
                    { 7, 1, 20, "TestUser" },
                    { 12, 2, 20, "TestUser" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                schema: "TestsDB",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TestId",
                schema: "TestsDB",
                table: "Results",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions",
                schema: "TestsDB");

            migrationBuilder.DropTable(
                name: "Results",
                schema: "TestsDB");

            migrationBuilder.DropTable(
                name: "Tests",
                schema: "TestsDB");
        }
    }
}
