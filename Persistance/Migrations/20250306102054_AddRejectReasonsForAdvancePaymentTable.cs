using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddRejectReasonsForAdvancePaymentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectReasons",
                table: "AdvancePayments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectReasons",
                table: "AdvancePayments");
        }
    }
}
