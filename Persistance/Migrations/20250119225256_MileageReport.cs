using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MileageReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "PrivateVehicleEngineSize",
                table: "BusinessTravels");
            migrationBuilder.DropColumn(
                name: "PrivateVehicleNumber",
                table: "BusinessTravels");
            migrationBuilder.AddColumn<string>(
                name: "MileageRegister",
                table: "BusinessTravels",
                type: "nvarchar(max)", 
                nullable: false,
                defaultValue: string.Empty);

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "TestForms",
            //    newName: "Statuses");

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "OnboardingForms",
            //    newName: "Statuses");

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "ITScrappingForms",
            //    newName: "Statuses");

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "ITSaleForms",
            //    newName: "Statuses");

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "DeferralPayments",
            //    newName: "Statuses");

            //migrationBuilder.RenameColumn(
            //    name: "PrivateVehicleNumber",
            //    table: "BusinessTravels",
            //    newName: "Statuses");

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "BusinessTravels",
            //    newName: "MileageRegister");

            //migrationBuilder.RenameColumn(
            //    name: "BusinessTravelStatuses",
            //    table: "AccountingNotes",
            //    newName: "Statuses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "TestForms",
            //    newName: "BusinessTravelStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "OnboardingForms",
            //    newName: "BusinessTravelStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "ITScrappingForms",
            //    newName: "BusinessTravelStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "ITSaleForms",
            //    newName: "BusinessTravelStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "DeferralPayments",
            //    newName: "BusinessTravelStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "BusinessTravels",
            //    newName: "PrivateVehicleNumber");

            //migrationBuilder.RenameColumn(
            //    name: "MileageRegister",
            //    table: "BusinessTravels",
            //    newName: "BusinessTravelStatuses");

            //migrationBuilder.RenameColumn(
            //    name: "Statuses",
            //    table: "AccountingNotes",
            //    newName: "BusinessTravelStatuses");

            migrationBuilder.AddColumn<int>(
                name: "PrivateVehicleEngineSize",
                table: "BusinessTravels",
                type: "int",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddColumn<int>(
                name: "PrivateVehicleNumber",
                table: "BusinessTravels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);


            migrationBuilder.AddColumn<string>(
                name: "MileageRegister",
                table: "BusinessTravels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
