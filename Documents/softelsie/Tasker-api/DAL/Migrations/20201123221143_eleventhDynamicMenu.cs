using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class eleventhDynamicMenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TTrackAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Action = table.Column<string>(nullable: true),
                    TaskId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TTrackAction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TTrackAction_TTask_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TUserType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    Discription = table.Column<string>(nullable: true),
                    DefaultUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Caption = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    HasSubs = table.Column<bool>(nullable: false),
                    UserTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TMenu_TUserType_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "TUserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TSubMenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Caption = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    MenuId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TSubMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TSubMenu_TMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "TMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TypeId",
                table: "AspNetUsers",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TMenu_UserTypeId",
                table: "TMenu",
                column: "UserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TSubMenu_MenuId",
                table: "TSubMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_TTrackAction_TaskId",
                table: "TTrackAction",
                column: "TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TUserType_TypeId",
                table: "AspNetUsers",
                column: "TypeId",
                principalTable: "TUserType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TUserType_TypeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TSubMenu");

            migrationBuilder.DropTable(
                name: "TTrackAction");

            migrationBuilder.DropTable(
                name: "TMenu");

            migrationBuilder.DropTable(
                name: "TUserType");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
