using Microsoft.EntityFrameworkCore.Migrations;

namespace payApp.API.Migrations
{
    public partial class WishEntityUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectedMoney",
                table: "Wishes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CollectedMoney",
                table: "Wishes");
        }
    }
}
