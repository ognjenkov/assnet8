using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class ShouldRefreshInvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShouldRefresh",
                table: "Invites",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShouldRefresh",
                table: "Invites");
        }
    }
}
