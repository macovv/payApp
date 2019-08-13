using Microsoft.EntityFrameworkCore.Migrations;

namespace payApp.API.Migrations
{
    public partial class Saldo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Saldo",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "AspNetUsers");
        }
    }
}
