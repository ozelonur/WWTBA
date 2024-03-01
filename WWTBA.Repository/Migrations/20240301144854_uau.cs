using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWTBA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class uau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTestId",
                table: "UserAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTestId",
                table: "UserAnswers");
        }
    }
}
