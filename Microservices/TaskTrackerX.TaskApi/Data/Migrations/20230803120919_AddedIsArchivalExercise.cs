using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerX.TaskApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsArchivalExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchival",
                table: "Exercise",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchival",
                table: "Exercise");
        }
    }
}
