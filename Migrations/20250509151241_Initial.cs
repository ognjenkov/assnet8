using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Invites",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Accepted = table.Column<bool>(type: "bit", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                ResponseDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Response = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Invites", x => x.Id);
                table.ForeignKey(
                    name: "FK_Invites_Teams_TeamId",
                    column: x => x.TeamId,
                    principalTable: "Teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Invites_Users_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "Users",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Invites_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id");
            });




        migrationBuilder.CreateIndex(
            name: "IX_Invites_CreatedById",
            table: "Invites",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_Invites_TeamId",
            table: "Invites",
            column: "TeamId");

        migrationBuilder.CreateIndex(
            name: "IX_Invites_UserId",
            table: "Invites",
            column: "UserId");


    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Invites");
    }
}
