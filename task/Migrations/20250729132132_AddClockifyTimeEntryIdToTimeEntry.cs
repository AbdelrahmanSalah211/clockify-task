using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task.Migrations
{
    /// <inheritdoc />
    public partial class AddClockifyTimeEntryIdToTimeEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockifyTimeEntryId",
                table: "TimeEntries",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockifyTimeEntryId",
                table: "TimeEntries");
        }
    }
}
