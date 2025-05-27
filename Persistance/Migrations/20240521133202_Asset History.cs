using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AssetHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssetTagNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AStateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ALongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ATypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AWarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BStateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BLongName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BWarehouseName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsHistory");
        }
    }
}
