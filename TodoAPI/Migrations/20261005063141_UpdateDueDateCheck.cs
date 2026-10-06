using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDueDateCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TodoItems_DueDate_NotPast",
                table: "TodoItems");

            migrationBuilder.AddCheckConstraint(
                name: "DueDateCannotBeInThePast",
                table: "TodoItems",
                sql: "[DueDate] >= CAST(GETDATE() AS DATE)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "DueDateCannotBeInThePast",
                table: "TodoItems");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TodoItems_DueDate_NotPast",
                table: "TodoItems",
                sql: "[DueDate] >= CAST(GETDATE() AS DATE)");
        }
    }
}
