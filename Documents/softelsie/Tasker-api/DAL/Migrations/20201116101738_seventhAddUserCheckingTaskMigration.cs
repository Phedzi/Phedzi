using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class seventhAddUserCheckingTaskMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTask_AspNetUsers_UserModelId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_UserModelId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "TTask");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "TTask",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTask_OwnerId",
                table: "TTask",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_AspNetUsers_OwnerId",
                table: "TTask",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTask_AspNetUsers_OwnerId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_OwnerId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "TTask");

            migrationBuilder.AddColumn<string>(
                name: "UserModelId",
                table: "TTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTask_UserModelId",
                table: "TTask",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_AspNetUsers_UserModelId",
                table: "TTask",
                column: "UserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
