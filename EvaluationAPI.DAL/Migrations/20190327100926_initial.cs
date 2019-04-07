using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EvaluationAPI.DAL.Migrations
{
    public partial class initial : Migration
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
                    Name = table.Column<string>(type: "varchar(25)", nullable: false),
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
