using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class userUnique1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("27633635-f9f4-44ea-8215-668000b8a351"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("96aae12b-1837-4a56-9b4c-3420767e9d36"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a93bef78-0efa-4999-9de9-050c883b97d3"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("b2868a9b-0697-4d25-aef7-c363c96899dc"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("e543e2f0-c734-4aa6-b9c4-9c8d79183842"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("fb6dfbab-19d0-4b03-b4d0-07d1f127b74e"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenCookie",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenApp",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Member" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "TeamLeader" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Creator" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Organizer" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "ServiceProvider" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "OrganizationOwner" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RefreshTokenApp",
                table: "Users",
                column: "RefreshTokenApp",
                unique: true,
                filter: "[RefreshTokenApp] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RefreshTokenCookie",
                table: "Users",
                column: "RefreshTokenCookie",
                unique: true,
                filter: "[RefreshTokenCookie] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RefreshTokenApp",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RefreshTokenCookie",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenCookie",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RefreshTokenApp",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
        }
    }
}
