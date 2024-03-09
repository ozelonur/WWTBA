using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WWTBA.Repository.Migrations
{
    /// <inheritdoc />
    public partial class vcodeexpire : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EmailVerificationCodeCreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmailVerificationCodeValidityDurationInMinutes",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerificationCodeCreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailVerificationCodeValidityDurationInMinutes",
                table: "Users");
        }
    }
}
