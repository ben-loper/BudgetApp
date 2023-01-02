using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBackend.Data.Migrations
{
    public partial class ChangesForFlyIo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMisc",
                table: "Budget",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMisc",
                table: "Budget");
        }
    }
}
