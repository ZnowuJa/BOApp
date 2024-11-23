using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFormFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormFiles");

            migrationBuilder.AddColumn<string>(
                name: "FormFiles",
                table: "ITSaleForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormFiles",
                table: "ITSaleForms");

            migrationBuilder.CreateTable(
                name: "FormFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    OnboardingFormId = table.Column<int>(type: "int", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestFormId = table.Column<int>(type: "int", nullable: true),
                    TmpFileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TmpFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TmpPath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormFiles_OnboardingForms_OnboardingFormId",
                        column: x => x.OnboardingFormId,
                        principalTable: "OnboardingForms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormFiles_TestForms_TestFormId",
                        column: x => x.TestFormId,
                        principalTable: "TestForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_OnboardingFormId",
                table: "FormFiles",
                column: "OnboardingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_TestFormId",
                table: "FormFiles",
                column: "TestFormId");
        }
    }
}
