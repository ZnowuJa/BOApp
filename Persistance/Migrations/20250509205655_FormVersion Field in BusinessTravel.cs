using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class FormVersionFieldinBusinessTravel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "AdvancePaymentAmount",
            //    table: "BankTransfers");

            //migrationBuilder.DropColumn(
            //    name: "AdvancePaymentBalance",
            //    table: "BankTransfers");

            //migrationBuilder.DropColumn(
            //    name: "AdvancePaymentCash",
            //    table: "BankTransfers");

            //migrationBuilder.DropColumn(
            //    name: "AdvancePaymentCurrency",
            //    table: "BankTransfers");

            //migrationBuilder.DropColumn(
            //    name: "AdvancePaymentDate",
            //    table: "BankTransfers");

            migrationBuilder.AddColumn<int>(
                name: "FormVersion",
                table: "BusinessTravels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "AdvancePaymentBalance",
            //    table: "AdvancePayments",
            //    type: "decimal(18,2)",
            //    nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormVersion",
                table: "BusinessTravels");

            //migrationBuilder.DropColumn(
            //    name: "AdvancePaymentBalance",
            //    table: "AdvancePayments");

            //migrationBuilder.AddColumn<decimal>(
            //    name: "AdvancePaymentAmount",
            //    table: "BankTransfers",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<decimal>(
            //    name: "AdvancePaymentBalance",
            //    table: "BankTransfers",
            //    type: "decimal(18,2)",
            //    nullable: true);

            //migrationBuilder.AddColumn<bool>(
            //    name: "AdvancePaymentCash",
            //    table: "BankTransfers",
            //    type: "bit",
            //    nullable: false,
            //    defaultValue: false);

            //migrationBuilder.AddColumn<string>(
            //    name: "AdvancePaymentCurrency",
            //    table: "BankTransfers",
            //    type: "nvarchar(max)",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateOnly>(
            //    name: "AdvancePaymentDate",
            //    table: "BankTransfers",
            //    type: "date",
            //    nullable: true);
        }
    }
}
