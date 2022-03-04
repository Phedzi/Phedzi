using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Budgetta13_ManytoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetModelId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TBudget",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsMonthly = table.Column<bool>(nullable: false),
                    SaveTarget = table.Column<decimal>(nullable: false),
                    SaveCurrent = table.Column<decimal>(nullable: false),
                    Loss = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBudget", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Descriotion = table.Column<string>(nullable: true),
                    HasTarget = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(nullable: false),
                    TotalTarget = table.Column<decimal>(nullable: false),
                    TotalLoss = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TItemType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TItemType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    PaymentMaid = table.Column<bool>(nullable: false),
                    PaidAmount = table.Column<decimal>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TItem_TItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "TItemType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBudgetCategory",
                columns: table => new
                {
                    BudgetId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    ItemModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBudgetCategory", x => new { x.BudgetId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_TBudgetCategory_TBudget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "TBudget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBudgetCategory_TCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBudgetCategory_TItem_ItemModelId",
                        column: x => x.ItemModelId,
                        principalTable: "TItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "TUserType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 4, 18, 38, 50, 959, DateTimeKind.Local).AddTicks(9710));

            migrationBuilder.UpdateData(
                table: "TUserType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 4, 18, 38, 50, 973, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BudgetModelId",
                table: "AspNetUsers",
                column: "BudgetModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TBudgetCategory_CategoryId",
                table: "TBudgetCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TBudgetCategory_ItemModelId",
                table: "TBudgetCategory",
                column: "ItemModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TItem_ItemTypeId",
                table: "TItem",
                column: "ItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_TBudget_BudgetModelId",
                table: "AspNetUsers",
                column: "BudgetModelId",
                principalTable: "TBudget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_TBudget_BudgetModelId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TBudgetCategory");

            migrationBuilder.DropTable(
                name: "TBudget");

            migrationBuilder.DropTable(
                name: "TCategory");

            migrationBuilder.DropTable(
                name: "TItem");

            migrationBuilder.DropTable(
                name: "TItemType");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BudgetModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BudgetModelId",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "TUserType",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 4, 17, 15, 41, 600, DateTimeKind.Local).AddTicks(4040));

            migrationBuilder.UpdateData(
                table: "TUserType",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2022, 3, 4, 17, 15, 41, 614, DateTimeKind.Local).AddTicks(9280));
        }
    }
}
