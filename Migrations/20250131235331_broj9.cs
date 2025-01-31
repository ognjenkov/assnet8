using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class broj9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Locations_LocationId1",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_LocationId1",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Municipalities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "Municipalities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_LocationId1",
                table: "Municipalities",
                column: "LocationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Locations_LocationId1",
                table: "Municipalities",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
