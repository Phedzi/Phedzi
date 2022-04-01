using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateTaskModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserModelId",
                table: "TTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountDepositedFriendlyName",
                table: "TTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountDueFriendlyName",
                table: "TTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountPaidFriendlyName",
                table: "TTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "TTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DueDateFriendlyName",
                table: "TTask",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTask_UserModelId",
                table: "TTask",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TTask_AssigneeId",
                table: "TTask",
                column: "AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_AspNetUsers_UserModelId",
                table: "TTask",
                column: "UserModelId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_AspNetUsers_AssigneeId",
                table: "TTask",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTask_AspNetUsers_UserModelId",
                table: "TTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TTask_AspNetUsers_AssigneeId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_UserModelId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_AssigneeId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "AmountDepositedFriendlyName",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "AmountDueFriendlyName",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "AmountPaidFriendlyName",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "DueDateFriendlyName",
                table: "TTask");
        }
    }
}
