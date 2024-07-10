using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class DepartmentaddSapNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "AssigneeVmId",
            //    table: "Assets");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Assets");

            migrationBuilder.AddColumn<string>(
                name: "SapNumber",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SapNumber",
                table: "Departments");

            //migrationBuilder.RenameColumn(
            //    name: "AssigneeId",
            //    table: "Assets",
            //    newName: "DepartmentId");

            //migrationBuilder.AddColumn<int>(
            //    name: "AssigneeVmId",
            //    table: "Assets",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);
        }
    }
}
