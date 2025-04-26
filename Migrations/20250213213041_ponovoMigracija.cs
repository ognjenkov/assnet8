using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class ponovoMigracija : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_MembershipRole_Role_RolesId",
            table: "MembershipRole");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Role",
            table: "Role");

        migrationBuilder.RenameTable(
            name: "Role",
            newName: "Roles");

        migrationBuilder.RenameIndex(
            name: "IX_Role_Name",
            table: "Roles",
            newName: "IX_Roles_Name");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Roles",
            table: "Roles",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_MembershipRole_Roles_RolesId",
            table: "MembershipRole",
            column: "RolesId",
            principalTable: "Roles",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_MembershipRole_Roles_RolesId",
            table: "MembershipRole");

        migrationBuilder.DropPrimaryKey(
            name: "PK_Roles",
            table: "Roles");

        migrationBuilder.RenameTable(
            name: "Roles",
            newName: "Role");

        migrationBuilder.RenameIndex(
            name: "IX_Roles_Name",
            table: "Role",
            newName: "IX_Role_Name");

        migrationBuilder.AddPrimaryKey(
            name: "PK_Role",
            table: "Role",
            column: "Id");

        migrationBuilder.AddForeignKey(
            name: "FK_MembershipRole_Role_RolesId",
            table: "MembershipRole",
            column: "RolesId",
            principalTable: "Role",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}