using Microsoft.EntityFrameworkCore.Migrations;

namespace MyResume.Migrations
{
    public partial class EducationsTableUpdated3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FinishedAt",
                table: "Educations",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FinishedAt",
                table: "Educations",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
