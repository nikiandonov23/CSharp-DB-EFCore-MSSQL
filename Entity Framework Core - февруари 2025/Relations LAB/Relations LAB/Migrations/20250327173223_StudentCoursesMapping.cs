using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relations_LAB.Migrations
{
    public partial class StudentCoursesMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_StudentId",
                schema: "uni",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentId",
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

            migrationBuilder.CreateTable(
                name: "StudentsCourses",
                schema: "uni",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsCourses", x => new { x.StudentId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "uni",
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "uni",
                        principalTable: "Students",
                        principalColumn: "StudentPk",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsCourses_CourseId",
                schema: "uni",
                table: "StudentsCourses",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsCourses",
                schema: "uni");

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

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentId",
                schema: "uni",
                table: "Students",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_StudentId",
                schema: "uni",
                table: "Students",
                column: "StudentId",
                principalSchema: "uni",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
