using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace payApp.API.Migrations
{
    public partial class WishEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Wishes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Finished",
                table: "Wishes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsFinished",
                table: "Wishes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "WishDescription",
                table: "Wishes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "Finished",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "IsFinished",
                table: "Wishes");

            migrationBuilder.DropColumn(
                name: "WishDescription",
                table: "Wishes");
        }
    }
}
