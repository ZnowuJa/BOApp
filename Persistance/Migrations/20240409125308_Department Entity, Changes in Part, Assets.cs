using System;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentEntityChangesinPartAssets : Migration
    {
        
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var now = DateTime.Now;
            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfSupport",
                table: "Parts",
                type: "datetime2",
                nullable: false, 
                defaultValue: new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfContract",
                table: "Assets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Leasing",
                table: "Assets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyUntil",
                table: "Assets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndOfSupport",
                table: "Parts");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "EndOfContract",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Leasing",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "WarrantyUntil",
                table: "Assets");
        }
    }
}
