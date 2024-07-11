using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class DeferralPaymentExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Role_SettlementsTeam",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LVL1_EmployeeName",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LVL1_EnovaEmpId",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LVL2_EmployeeName",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LVL2_EnovaEmpId",
                table: "DeferralPayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "Role_SettlementsTeam",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Approvals",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "LVL1_EmployeeName",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "LVL1_EnovaEmpId",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "LVL2_EmployeeName",
                table: "DeferralPayments");

            migrationBuilder.DropColumn(
                name: "LVL2_EnovaEmpId",
                table: "DeferralPayments");

            
        }
    }
}
