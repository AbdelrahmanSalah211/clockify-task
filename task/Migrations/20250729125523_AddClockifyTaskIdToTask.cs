using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task.Migrations
{
    /// <inheritdoc />
    public partial class AddClockifyTaskIdToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockifyTaskId",
                table: "Tasks",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockifyTaskId",
                table: "Tasks");
        }
    }
}
