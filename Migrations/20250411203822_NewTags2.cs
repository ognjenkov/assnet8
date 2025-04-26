using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class NewTags2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Tags",
            columns: new[] { "Id", "Name", "Type" },
            values: new object[,]
            {
                { new Guid("11111111-1111-1111-1111-111111111180"), "Assault Rifle", "Listing" },
                { new Guid("11111111-1111-1111-1111-111111111181"), "Great Renting", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111182"), "Birthdays", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111183"), "Private games", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111184"), "Open games", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111185"), "Shop", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111186"), "HPA Service", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111187"), "GBB Service", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111188"), "AEG Service", "Service" },
                { new Guid("11111111-1111-1111-1111-111111111189"), "Pistol", "Listing" },
                { new Guid("11111111-1111-1111-1111-111111111191"), "Attachment", "Listing" },
                { new Guid("11111111-1111-1111-1111-111111111192"), "Uniform", "Listing" },
                { new Guid("11111111-1111-1111-1111-111111111193"), "Gear", "Listing" },
                { new Guid("11111111-1111-1111-1111-111111111194"), "Replica", "Listing" },
                { new Guid("11111111-1111-1111-1111-111111111195"), "Milsim", "Game" },
                { new Guid("11111111-1111-1111-1111-111111111196"), "Outdoors", "Game" },
                { new Guid("11111111-1111-1111-1111-111111111197"), "CQB", "Game" }
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111180"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111181"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111182"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111183"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111184"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111185"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111186"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111187"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111188"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111189"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111191"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111192"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111193"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111194"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111195"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111196"));

        migrationBuilder.DeleteData(
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111197"));
    }
}