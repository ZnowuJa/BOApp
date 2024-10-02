using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InstructionStatusinOnboardingForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructions_OnboardingForms_OnboardingFormId",
                table: "Instructions");

            migrationBuilder.DropIndex(
                name: "IX_Instructions_OnboardingFormId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "OnboardingFormId",
                table: "Instructions");

            migrationBuilder.AddColumn<string>(
                name: "Role_ComplianceManager",
                table: "Organisations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Instructions",
                table: "OnboardingForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role_ComplianceManager",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Instructions",
                table: "OnboardingForms");

            migrationBuilder.AddColumn<int>(
                name: "OnboardingFormId",
                table: "Instructions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_OnboardingFormId",
                table: "Instructions",
                column: "OnboardingFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructions_OnboardingForms_OnboardingFormId",
                table: "Instructions",
                column: "OnboardingFormId",
                principalTable: "OnboardingForms",
                principalColumn: "Id");
        }
    }
}
