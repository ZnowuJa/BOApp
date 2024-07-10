using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangesinEmployeeVmAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "Employees",
                newName: "VcdempNumber");

            migrationBuilder.RenameColumn(
                name: "EmpId",
                table: "Employees",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "DG",
                table: "Employees",
                newName: "VcdempId");

            migrationBuilder.RenameColumn(
                name: "CC",
                table: "Employees",
                newName: "VcddeptNumber");

            migrationBuilder.AddColumn<Guid>(
                name: "AzureObjectId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "EnovaEmpId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SapNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VcdCompanyNr",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AzureObjectId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AzureObjectId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EnovaEmpId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SapNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "VcdCompanyNr",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AzureObjectId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VcdempNumber",
                table: "Employees",
                newName: "IdentityUserId");

            migrationBuilder.RenameColumn(
                name: "VcdempId",
                table: "Employees",
                newName: "DG");

            migrationBuilder.RenameColumn(
                name: "VcddeptNumber",
                table: "Employees",
                newName: "CC");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Employees",
                newName: "EmpId");
        }
    }
}
