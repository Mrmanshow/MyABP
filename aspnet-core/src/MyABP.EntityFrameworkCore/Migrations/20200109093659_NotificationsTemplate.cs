using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyABP.Migrations
{
    public partial class NotificationsTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "abp_notifications_template",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    creation_time = table.Column<DateTime>(nullable: false),
                    creator_user_id = table.Column<long>(nullable: true),
                    last_modification_time = table.Column<DateTime>(nullable: true),
                    last_modifier_user_id = table.Column<long>(nullable: true),
                    is_deleted = table.Column<bool>(nullable: false),
                    deleter_user_id = table.Column<long>(nullable: true),
                    deletion_time = table.Column<DateTime>(nullable: true),
                    notification_name = table.Column<string>(maxLength: 96, nullable: false),
                    display_name = table.Column<string>(maxLength: 50, nullable: true),
                    content = table.Column<string>(maxLength: 200, nullable: true),
                    subscribed_number = table.Column<int>(nullable: false),
                    push_number = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_abp_notifications_template", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "abp_notifications_template");
        }
    }
}
