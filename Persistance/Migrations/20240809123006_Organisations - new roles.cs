using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class Organisationsnewroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role_Accountants",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_AccountantsTeamLeader",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_Cashiers",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_HRSpecialists",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_InvestmentsDept",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role_SourcingDept",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_Accountants",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Role_AccountantsTeamLeader",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Role_Cashiers",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Role_HRSpecialists",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Role_InvestmentsDept",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Role_SourcingDept",
                table: "Organisations");
        }
    }
}
