using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerX.AuthApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsArchivalUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchival",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchival",
                table: "AspNetUsers");
        }
    }
}
