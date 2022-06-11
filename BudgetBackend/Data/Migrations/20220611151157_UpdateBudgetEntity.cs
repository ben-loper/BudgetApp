using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetBackend.Data.Migrations
{
    public partial class UpdateBudgetEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Budget",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "WeeklyBudget");

            migrationBuilder.CreateTable(
                name: "Budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonthlyIncomeId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsWeekly = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_MonthlyIncome",
                        column: x => x.MonthlyIncomeId,
                        principalTable: "MonthlyIncome",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget",
                table: "Budget",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyBudget_MonthlyIncomeId",
                table: "Budget",
                column: "MonthlyIncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Budget",
                table: "Transaction",
                column: "BudgetId",
                principalTable: "Budget",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Budget",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Budget");

            migrationBuilder.CreateTable(
                name: "WeeklyBudget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonthlyIncomeId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyBudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_MonthlyIncome",
                        column: x => x.MonthlyIncomeId,
                        principalTable: "MonthlyIncome",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Budget",
                table: "WeeklyBudget",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyBudget_MonthlyIncomeId",
                table: "WeeklyBudget",
                column: "MonthlyIncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Budget",
                table: "Transaction",
                column: "BudgetId",
                principalTable: "WeeklyBudget",
                principalColumn: "Id");
        }
    }
}
