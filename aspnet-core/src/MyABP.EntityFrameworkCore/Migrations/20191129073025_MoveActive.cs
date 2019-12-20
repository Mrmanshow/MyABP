using Microsoft.EntityFrameworkCore.Migrations;

namespace MyABP.Migrations
{
    public partial class MoveActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LabaWinRoute");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LabaOrderDetail");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "LabaOrder");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LabaWinRoute",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LabaOrderDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LabaOrder",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "LabaWinRoute");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LabaOrderDetail");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LabaOrder");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LabaWinRoute",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LabaOrderDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "LabaOrder",
                nullable: false,
                defaultValue: false);
        }
    }
}
