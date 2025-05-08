using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class abc123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Teams_TeamId",
                table: "Invites");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Teams_TeamId",
                table: "Invites",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_Teams_TeamId",
                table: "Invites");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_Teams_TeamId",
                table: "Invites",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
