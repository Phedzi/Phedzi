using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AssignTaskToAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgreementId",
                table: "TTask",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTask_AgreementId",
                table: "TTask",
                column: "AgreementId");

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_TAgreement_AgreementId",
                table: "TTask",
                column: "AgreementId",
                principalTable: "TAgreement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTask_TAgreement_AgreementId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_AgreementId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "AgreementId",
                table: "TTask");
        }
    }
}
