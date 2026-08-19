using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseLevelAndRemoveGrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grades",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "Credits",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "PricePerHour",
                table: "Course");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "InstructorCourses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CourseLevel",
                table: "Course",
                type: "NVarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Course",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseLevel",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Course");

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "InstructorCourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Grades",
                table: "Enrollment",
                type: "Decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Credits",
                table: "Course",
                type: "Int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerHour",
                table: "Course",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }
    }
}
