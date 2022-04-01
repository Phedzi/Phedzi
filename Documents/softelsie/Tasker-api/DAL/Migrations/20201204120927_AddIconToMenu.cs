using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddIconToMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "TSubMenu",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "TMenu",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "TSubMenu");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "TMenu");
        }
    }
}
