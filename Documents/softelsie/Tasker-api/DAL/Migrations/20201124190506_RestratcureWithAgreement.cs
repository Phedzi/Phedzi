using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RestratcureWithAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TTask_AspNetUsers_AssigneeId",
                table: "TTask");

            migrationBuilder.DropForeignKey(
                name: "FK_TTask_AspNetUsers_OwnerId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_AssigneeId",
                table: "TTask");

            migrationBuilder.DropIndex(
                name: "IX_TTask_OwnerId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "TTask");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "TTask");

            migrationBuilder.CreateTable(
                name: "TAgreementType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAgreementType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TAgreement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    AgreementTypeId = table.Column<int>(nullable: true),
                    DateStart = table.Column<DateTime>(nullable: false),
                    DateEnd = table.Column<DateTime>(nullable: false),
                    NeedsAprooval = table.Column<bool>(nullable: false),
                    IsReopenable = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    AssigneeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAgreement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TAgreement_TAgreementType_AgreementTypeId",
                        column: x => x.AgreementTypeId,
                        principalTable: "TAgreementType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TAgreement_AspNetUsers_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TAgreement_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TAgreement_AgreementTypeId",
                table: "TAgreement",
                column: "AgreementTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TAgreement_AssigneeId",
                table: "TAgreement",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_TAgreement_OwnerId",
                table: "TAgreement",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TAgreement");

            migrationBuilder.DropTable(
                name: "TAgreementType");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeId",
                table: "TTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "TTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TTask_AssigneeId",
                table: "TTask",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_TTask_OwnerId",
                table: "TTask",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_AspNetUsers_AssigneeId",
                table: "TTask",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TTask_AspNetUsers_OwnerId",
                table: "TTask",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
