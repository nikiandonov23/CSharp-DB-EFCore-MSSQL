using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relations_LAB.Migrations
{
    public partial class RoomsAddedFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "uni",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                schema: "uni",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                schema: "uni",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                schema: "uni",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "uni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Town = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PostCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    AddressLine = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StudentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "uni",
                        principalTable: "Students",
                        principalColumn: "StudentPk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "uni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_RoomId",
                schema: "uni",
                table: "Students",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId",
                schema: "uni",
                table: "Students",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StudentId",
                schema: "uni",
                table: "Addresses",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_StudentId",
                schema: "uni",
                table: "Students",
                column: "StudentId",
                principalSchema: "uni",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Rooms_RoomId",
                schema: "uni",
                table: "Students",
                column: "RoomId",
                principalSchema: "uni",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_StudentId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Rooms_RoomId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "uni");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "uni");

            migrationBuilder.DropIndex(
                name: "IX_Students_RoomId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "RoomId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                schema: "uni",
                table: "Courses");
        }
    }
}
