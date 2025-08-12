using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relations_LAB.Migrations
{
    public partial class StudentNameLenghtChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "uni",
                table: "Students",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                comment: "Imeto na studenta",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Imeto na studenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "uni",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Imeto na studenta",
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldComment: "Imeto na studenta");
        }
    }
}
