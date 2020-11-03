using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ThirdVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<ulong>(
                name: "Welcome",
                table: "Servers",
                nullable: false,
                defaultValue: 0ul);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Welcome",
                table: "Servers");
        }
    }
}
