using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relations_LAB.Migrations
{
    public partial class CoursesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "uni",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "The name of the current course"),
                    CourseStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseEnd = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                },
                comment: "Courses in university");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses",
                schema: "uni");
        }
    }
}
