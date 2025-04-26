using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class newRoles1 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Role",
            table: "Memberships");

        migrationBuilder.CreateTable(
            name: "Role",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Role", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "MembershipRole",
            columns: table => new
            {
                MembershipsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                RolesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MembershipRole", x => new { x.MembershipsId, x.RolesId });
                table.ForeignKey(
                    name: "FK_MembershipRole_Memberships_MembershipsId",
                    column: x => x.MembershipsId,
                    principalTable: "Memberships",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_MembershipRole_Role_RolesId",
                    column: x => x.RolesId,
                    principalTable: "Role",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Role",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { new Guid("27633635-f9f4-44ea-8215-668000b8a351"), "OrganizationOwner" },
                { new Guid("96aae12b-1837-4a56-9b4c-3420767e9d36"), "TeamLeader" },
                { new Guid("a93bef78-0efa-4999-9de9-050c883b97d3"), "Creator" },
                { new Guid("b2868a9b-0697-4d25-aef7-c363c96899dc"), "ServiceProvider" },
                { new Guid("e543e2f0-c734-4aa6-b9c4-9c8d79183842"), "Member" },
                { new Guid("fb6dfbab-19d0-4b03-b4d0-07d1f127b74e"), "Organizer" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_MembershipRole_RolesId",
            table: "MembershipRole",
            column: "RolesId");

        migrationBuilder.CreateIndex(
            name: "IX_Role_Name",
            table: "Role",
            column: "Name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "MembershipRole");

        migrationBuilder.DropTable(
            name: "Role");

        migrationBuilder.AddColumn<string>(
            name: "Role",
            table: "Memberships",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}