using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class membershipTeamFkFix1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Memberships_Teams_UserId",
            table: "Memberships");

        migrationBuilder.CreateIndex(
            name: "IX_Memberships_TeamId",
            table: "Memberships",
            column: "TeamId");

        migrationBuilder.AddForeignKey(
            name: "FK_Memberships_Teams_TeamId",
            table: "Memberships",
            column: "TeamId",
            principalTable: "Teams",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Memberships_Teams_TeamId",
            table: "Memberships");

        migrationBuilder.DropIndex(
            name: "IX_Memberships_TeamId",
            table: "Memberships");

        migrationBuilder.AddForeignKey(
            name: "FK_Memberships_Teams_UserId",
            table: "Memberships",
            column: "UserId",
            principalTable: "Teams",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}