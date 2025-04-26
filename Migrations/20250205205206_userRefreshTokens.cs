using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class userRefreshTokens : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "PersistLogin",
            table: "Users",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "RefreshTokenApp",
            table: "Users",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "RefreshTokenCookie",
            table: "Users",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "PersistLogin",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "RefreshTokenApp",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "RefreshTokenCookie",
            table: "Users");
    }
}