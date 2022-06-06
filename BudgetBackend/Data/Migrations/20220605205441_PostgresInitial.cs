using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetBackend.Data.Migrations
{
    public partial class PostgresInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonthlyIncome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false),
                    LastPayDay = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyIncome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonthlyBill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    MonthlyIncomeId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyBill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthlyBill_MonthlyIncome",
                        column: x => x.MonthlyIncomeId,
                        principalTable: "MonthlyIncome",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeeklyBudget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonthlyIncomeId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", unicode: false, maxLength: 50, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BudgetId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "(current_timestamp)"),
                    TransactionDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Budget",
                        column: x => x.BudgetId,
                        principalTable: "WeeklyBudget",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyBill_MonthlyIncomeId",
                table: "MonthlyBill",
                column: "MonthlyIncomeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BudgetId",
                table: "Transaction",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget",
                table: "WeeklyBudget",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyBudget_MonthlyIncomeId",
                table: "WeeklyBudget",
                column: "MonthlyIncomeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyBill");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "WeeklyBudget");

            migrationBuilder.DropTable(
                name: "MonthlyIncome");
        }
    }
}
