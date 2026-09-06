using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUserLinkAndOtp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Instructor",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Instructor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Student_ApplicationUserId",
                table: "Student",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Instructor_ApplicationUserId",
                table: "Instructor",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_AspNetUsers_ApplicationUserId",
                table: "Instructor",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_ApplicationUserId",
                table: "Student",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_AspNetUsers_ApplicationUserId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_ApplicationUserId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_ApplicationUserId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Instructor_ApplicationUserId",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Instructor");
        }
    }
}
