using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProjeKampi.WebApi.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_yummyEvents",
                table: "yummyEvents");

            migrationBuilder.RenameTable(
                name: "yummyEvents",
                newName: "YummyEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_YummyEvents",
                table: "YummyEvents",
                column: "YummyEventId");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_YummyEvents",
                table: "YummyEvents");

            migrationBuilder.RenameTable(
                name: "YummyEvents",
                newName: "yummyEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_yummyEvents",
                table: "yummyEvents",
                column: "YummyEventId");
        }
    }
}
