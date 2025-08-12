using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cadastre.Migrations
{
    public partial class initialMigration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CitizenProperty");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CitizenProperty",
                columns: table => new
                {
                    PropertiesCitizensId = table.Column<int>(type: "int", nullable: false),
                    PropertiesCitizensId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CitizenProperty", x => new { x.PropertiesCitizensId, x.PropertiesCitizensId1 });
                    table.ForeignKey(
                        name: "FK_CitizenProperty_Citizens_PropertiesCitizensId",
                        column: x => x.PropertiesCitizensId,
                        principalTable: "Citizens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CitizenProperty_Properties_PropertiesCitizensId1",
                        column: x => x.PropertiesCitizensId1,
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CitizenProperty_PropertiesCitizensId1",
                table: "CitizenProperty",
                column: "PropertiesCitizensId1");
        }
    }
}
