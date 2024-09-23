using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class CoCwithManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Instructions_InstructionCoCId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_InstructionCoCId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "InstructionCoCId",
                table: "Groups");

            migrationBuilder.CreateTable(
                name: "InstructionGroup",
                columns: table => new
                {
                    GroupsId = table.Column<int>(type: "int", nullable: false),
                    InstructionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionGroup", x => new { x.GroupsId, x.InstructionsId });
                    table.ForeignKey(
                        name: "FK_InstructionGroup_Groups_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_InstructionGroup_Instructions_InstructionsId",
                        column: x => x.InstructionsId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstructionGroup_InstructionsId",
                table: "InstructionGroup",
                column: "InstructionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstructionGroup");

            migrationBuilder.AddColumn<int>(
                name: "InstructionCoCId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_InstructionCoCId",
                table: "Groups",
                column: "InstructionCoCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Instructions_InstructionCoCId",
                table: "Groups",
                column: "InstructionCoCId",
                principalTable: "Instructions",
                principalColumn: "Id");
        }
    }
}
