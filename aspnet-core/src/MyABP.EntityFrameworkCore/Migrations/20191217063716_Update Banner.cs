using Microsoft.EntityFrameworkCore.Migrations;

namespace MyABP.Migrations
{
    public partial class UpdateBanner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LinkType",
                table: "Banner",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LinkType",
                table: "Banner",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
