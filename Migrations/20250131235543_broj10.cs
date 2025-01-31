using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class broj10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Organizations_OrganizationId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Services_ServiceId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Teams_TeamId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Galleries_GalleryId1",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Galleries_GalleryId1",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_GalleryId1",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Listings_GalleryId1",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Images_OrganizationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ServiceId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TeamId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "GalleryId1",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "GalleryId1",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Images");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GalleryId1",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GalleryId1",
                table: "Listings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_GalleryId1",
                table: "Services",
                column: "GalleryId1",
                unique: true,
                filter: "[GalleryId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_GalleryId1",
                table: "Listings",
                column: "GalleryId1",
                unique: true,
                filter: "[GalleryId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_OrganizationId",
                table: "Images",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ServiceId",
                table: "Images",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TeamId",
                table: "Images",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Organizations_OrganizationId",
                table: "Images",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Services_ServiceId",
                table: "Images",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Teams_TeamId",
                table: "Images",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Galleries_GalleryId1",
                table: "Listings",
                column: "GalleryId1",
                principalTable: "Galleries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Galleries_GalleryId1",
                table: "Services",
                column: "GalleryId1",
                principalTable: "Galleries",
                principalColumn: "Id");
        }
    }
}
