using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class AddToOrderTotalAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAmount",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AuthorInPrintingEditions",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { 1, 1 },
                column: "Date",
                value: new DateTime(2020, 12, 10, 12, 21, 15, 534, DateTimeKind.Local).AddTicks(4813));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "AuthorInPrintingEditions",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { 1, 1 },
                column: "Date",
                value: new DateTime(2020, 11, 13, 16, 13, 27, 55, DateTimeKind.Local).AddTicks(4733));
        }
    }
}
