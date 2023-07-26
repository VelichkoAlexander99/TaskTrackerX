using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskTrackerX.Database.Migrations
{
    /// <inheritdoc />
    public partial class RelationshipUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskInfos_Users_AssignedToUserId",
                schema: "taskTrackerX",
                table: "TaskInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskInfos_Users_AssignedToUserId",
                schema: "taskTrackerX",
                table: "TaskInfos",
                column: "AssignedToUserId",
                principalSchema: "taskTrackerX",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskInfos_Users_AssignedToUserId",
                schema: "taskTrackerX",
                table: "TaskInfos");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskInfos_Users_AssignedToUserId",
                schema: "taskTrackerX",
                table: "TaskInfos",
                column: "AssignedToUserId",
                principalSchema: "taskTrackerX",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
