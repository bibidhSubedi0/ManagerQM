using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManagerQM.Migrations
{
    /// <inheritdoc />
    public partial class AddUserEmailToUserTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Tasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Tasks");
        }
    }
}
