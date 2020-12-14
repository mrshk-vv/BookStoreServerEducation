using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.DataAccess.Migrations
{
    public partial class ChangeDataTypeInTotalAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AuthorInPrintingEditions",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { 1, 1 },
                column: "Date",
                value: new DateTime(2020, 12, 10, 12, 34, 52, 994, DateTimeKind.Local).AddTicks(8472));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalAmount",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.UpdateData(
                table: "AuthorInPrintingEditions",
                keyColumns: new[] { "AuthorId", "PrintingEditionId" },
                keyValues: new object[] { 1, 1 },
                column: "Date",
                value: new DateTime(2020, 12, 10, 12, 21, 15, 534, DateTimeKind.Local).AddTicks(4813));
        }
    }
}
