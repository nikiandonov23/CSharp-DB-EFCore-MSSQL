using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadastre.Migrations
{
    public partial class initialMigration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCitizen_Citizens_CitizenId",
                table: "PropertyCitizen");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyCitizen_Properties_PropertyId",
                table: "PropertyCitizen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyCitizen",
                table: "PropertyCitizen");

            migrationBuilder.RenameTable(
                name: "PropertyCitizen",
                newName: "PropertiesCitizens");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyCitizen_CitizenId",
                table: "PropertiesCitizens",
                newName: "IX_PropertiesCitizens_CitizenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertiesCitizens",
                table: "PropertiesCitizens",
                columns: new[] { "PropertyId", "CitizenId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesCitizens_Citizens_CitizenId",
                table: "PropertiesCitizens",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertiesCitizens_Properties_PropertyId",
                table: "PropertiesCitizens",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesCitizens_Citizens_CitizenId",
                table: "PropertiesCitizens");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertiesCitizens_Properties_PropertyId",
                table: "PropertiesCitizens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertiesCitizens",
                table: "PropertiesCitizens");

            migrationBuilder.RenameTable(
                name: "PropertiesCitizens",
                newName: "PropertyCitizen");

            migrationBuilder.RenameIndex(
                name: "IX_PropertiesCitizens_CitizenId",
                table: "PropertyCitizen",
                newName: "IX_PropertyCitizen_CitizenId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyCitizen",
                table: "PropertyCitizen",
                columns: new[] { "PropertyId", "CitizenId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCitizen_Citizens_CitizenId",
                table: "PropertyCitizen",
                column: "CitizenId",
                principalTable: "Citizens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyCitizen_Properties_PropertyId",
                table: "PropertyCitizen",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
