using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWTBA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class pr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordResetCode",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PasswordResetCodeCreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PasswordResetCodeValidityDurationInMinutes",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordResetCode",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeCreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordResetCodeValidityDurationInMinutes",
                table: "Users");
        }
    }
}
