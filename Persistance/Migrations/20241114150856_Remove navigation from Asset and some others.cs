using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Graph.DeviceManagement.ApplePushNotificationCertificate.DownloadApplePushNotificationCertificateSigningRequest;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemovenavigationfromAssetandsomeothers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ITSaleForms_SaleFormId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_ITScrappingForms_ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_ITSaleForms_ITSaleFormId",
                table: "FormFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_FormFiles_ITScrappingForms_ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ITScrappingForms_Companies_CompanyId",
                table: "ITScrappingForms");

            migrationBuilder.DropIndex(
                name: "IX_ITScrappingForms_CompanyId",
                table: "ITScrappingForms");

            migrationBuilder.DropIndex(
                name: "IX_FormFiles_ITSaleFormId",
                table: "FormFiles");

            migrationBuilder.DropIndex(
                name: "IX_FormFiles_ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.DropIndex(
                name: "IX_Assets_SaleFormId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_ScrappingFormId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "ITSaleFormId",
                table: "FormFiles");

            migrationBuilder.DropColumn(
                name: "ITScrappingFormId",
                table: "FormFiles");

            migrationBuilder.AddColumn<string>(
                name: "Assets",
                table: "ITScrappingForms",
                type: "nvarchar(max)",
                nullable: true);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Assets",
                table: "ITScrappingForms");

            migrationBuilder.AddColumn<int>(
                name: "ITSaleFormId",
                table: "FormFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ITScrappingFormId",
                table: "FormFiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ITScrappingForms_CompanyId",
                table: "ITScrappingForms",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_ITSaleFormId",
                table: "FormFiles",
                column: "ITSaleFormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormFiles_ITScrappingFormId",
                table: "FormFiles",
                column: "ITScrappingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_SaleFormId",
                table: "Assets",
                column: "SaleFormId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ScrappingFormId",
                table: "Assets",
                column: "ScrappingFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ITSaleForms_SaleFormId",
                table: "Assets",
                column: "SaleFormId",
                principalTable: "ITSaleForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_ITScrappingForms_ScrappingFormId",
                table: "Assets",
                column: "ScrappingFormId",
                principalTable: "ITScrappingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_ITSaleForms_ITSaleFormId",
                table: "FormFiles",
                column: "ITSaleFormId",
                principalTable: "ITSaleForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormFiles_ITScrappingForms_ITScrappingFormId",
                table: "FormFiles",
                column: "ITScrappingFormId",
                principalTable: "ITScrappingForms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ITScrappingForms_Companies_CompanyId",
                table: "ITScrappingForms",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}


//UP
//migrationBuilder.CreateTable(
//    name: "CostCenters",
//    columns: table => new
//    {
//        Id = table.Column<int>(type: "int", nullable: false)
//            .Annotation("SqlServer:Identity", "1, 1"),
//        MPK = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        StatusId = table.Column<int>(type: "int", nullable: true),
//        InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
//        Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true)
//    },
//    constraints: table =>
//    {
//        table.PrimaryKey("PK_CostCenters", x => x.Id);
//    });

//migrationBuilder.CreateTable(
//    name: "Countries",
//    columns: table => new
//    {
//        Id = table.Column<int>(type: "int", nullable: false)
//            .Annotation("SqlServer:Identity", "1, 1"),
//        Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        IsEU = table.Column<bool>(type: "bit", nullable: false),
//        StatusId = table.Column<int>(type: "int", nullable: true),
//        InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
//        Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true)
//    },
//    constraints: table =>
//    {
//        table.PrimaryKey("PK_Countries", x => x.Id);
//    });

//migrationBuilder.CreateTable(
//    name: "GLAccounts",
//    columns: table => new
//    {
//        Id = table.Column<int>(type: "int", nullable: false)
//            .Annotation("SqlServer:Identity", "1, 1"),
//        AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        StatusId = table.Column<int>(type: "int", nullable: true),
//        InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
//        Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true)
//    },
//    constraints: table =>
//    {
//        table.PrimaryKey("PK_GLAccounts", x => x.Id);
//    });

//migrationBuilder.CreateTable(
//    name: "VATRates",
//    columns: table => new
//    {
//        Id = table.Column<int>(type: "int", nullable: false)
//            .Annotation("SqlServer:Identity", "1, 1"),
//        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        Percentage = table.Column<double>(type: "float", nullable: false),
//        Information = table.Column<string>(type: "nvarchar(max)", nullable: false),
//        Order = table.Column<int>(type: "int", nullable: false),
//        StatusId = table.Column<int>(type: "int", nullable: true),
//        InactivatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
//        Inactivated = table.Column<DateTime>(type: "datetime2", nullable: true)
//    },
//    constraints: table =>
//    {
//        table.PrimaryKey("PK_VATRates", x => x.Id);
//    });


//DOWN
//migrationBuilder.DropTable(
//                name: "CostCenters");

//migrationBuilder.DropTable(
//    name: "Countries");

//migrationBuilder.DropTable(
//    name: "GLAccounts");

//migrationBuilder.DropTable(
//    name: "VATRates");
