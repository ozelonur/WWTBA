using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWTBA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class uawqt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "QuestionSolveTime",
                table: "UserAnswers",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionSolveTime",
                table: "UserAnswers");
        }
    }
}
