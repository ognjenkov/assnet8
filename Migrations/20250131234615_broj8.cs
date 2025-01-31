using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class broj8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Galleries_GalleryId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Images_ThumbnailImageId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Locations_LocationId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Organizations_OrganizationId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Services_ServiceId",
                table: "Galleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Fields_FieldId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Galleries_GalleryId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Galleries_GalleryId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Locations_LocationId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Images_LogoImageId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Images_ThumbnailImageId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Locations_LocationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Users_UserId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_LogoImageId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Organizations_OrganizationId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Teams_TeamId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "GalleryTeam");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_TeamId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Services_UserId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_TeamId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_ServiceId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganizationType",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Galleries");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Teams",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_OrganizationId",
                table: "Teams",
                newName: "IX_Teams_LocationId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Services",
                newName: "GalleryId1");

            migrationBuilder.RenameColumn(
                name: "TeamRole",
                table: "Memberships",
                newName: "Role");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "Teams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId1",
                table: "Municipalities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Listings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GalleryId1",
                table: "Listings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ListingId",
                table: "Images",
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

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Galleries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GameId",
                table: "Entries",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatorId",
                table: "Teams",
                column: "CreatorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_GalleryId",
                table: "Services",
                column: "GalleryId",
                unique: true,
                filter: "[GalleryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Services_GalleryId1",
                table: "Services",
                column: "GalleryId1",
                unique: true,
                filter: "[GalleryId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_TeamId",
                table: "Organizations",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_LocationId1",
                table: "Municipalities",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Listings_GalleryId1",
                table: "Listings",
                column: "GalleryId1",
                unique: true,
                filter: "[GalleryId1] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ListingId",
                table: "Images",
                column: "ListingId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_TeamId",
                table: "Galleries",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_GameId",
                table: "Entries",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Games_GameId",
                table: "Entries",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Galleries_GalleryId",
                table: "Fields",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Images_ThumbnailImageId",
                table: "Fields",
                column: "ThumbnailImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Locations_LocationId",
                table: "Fields",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Organizations_OrganizationId",
                table: "Fields",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Teams_TeamId",
                table: "Galleries",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Fields_FieldId",
                table: "Games",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Galleries_GalleryId",
                table: "Images",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images",
                column: "ListingId",
                principalTable: "Listings",
                principalColumn: "Id");

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
                name: "FK_Listings_Galleries_GalleryId",
                table: "Listings",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Galleries_GalleryId1",
                table: "Listings",
                column: "GalleryId1",
                principalTable: "Galleries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Locations_LocationId",
                table: "Listings",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Municipalities_Locations_LocationId1",
                table: "Municipalities",
                column: "LocationId1",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Images_LogoImageId",
                table: "Organizations",
                column: "LogoImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Galleries_GalleryId",
                table: "Services",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Galleries_GalleryId1",
                table: "Services",
                column: "GalleryId1",
                principalTable: "Galleries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Images_ThumbnailImageId",
                table: "Services",
                column: "ThumbnailImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Locations_LocationId",
                table: "Services",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_LogoImageId",
                table: "Teams",
                column: "LogoImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Locations_LocationId",
                table: "Teams",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Users_CreatorId",
                table: "Teams",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Games_GameId",
                table: "Entries");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Galleries_GalleryId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Images_ThumbnailImageId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Locations_LocationId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Organizations_OrganizationId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Galleries_Teams_TeamId",
                table: "Galleries");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Fields_FieldId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Galleries_GalleryId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Listings_ListingId",
                table: "Images");

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
                name: "FK_Listings_Galleries_GalleryId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Galleries_GalleryId1",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Listings_Locations_LocationId",
                table: "Listings");

            migrationBuilder.DropForeignKey(
                name: "FK_Municipalities_Locations_LocationId1",
                table: "Municipalities");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Images_LogoImageId",
                table: "Organizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Galleries_GalleryId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Galleries_GalleryId1",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Images_ThumbnailImageId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Locations_LocationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Images_LogoImageId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Locations_LocationId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Users_CreatorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CreatorId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Services_GalleryId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_GalleryId1",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_TeamId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Municipalities_LocationId1",
                table: "Municipalities");

            migrationBuilder.DropIndex(
                name: "IX_Listings_GalleryId1",
                table: "Listings");

            migrationBuilder.DropIndex(
                name: "IX_Images_ListingId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_OrganizationId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ServiceId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TeamId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Galleries_TeamId",
                table: "Galleries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_GameId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LocationId1",
                table: "Municipalities");

            migrationBuilder.DropColumn(
                name: "GalleryId1",
                table: "Listings");

            migrationBuilder.DropColumn(
                name: "ListingId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Galleries");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Entries");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Teams",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_LocationId",
                table: "Teams",
                newName: "IX_Teams_OrganizationId");

            migrationBuilder.RenameColumn(
                name: "GalleryId1",
                table: "Services",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Memberships",
                newName: "TeamRole");

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeamId",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationType",
                table: "Organizations",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Listings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServiceId",
                table: "Galleries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "GalleryTeam",
                columns: table => new
                {
                    GalleriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryTeam", x => new { x.GalleriesId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_GalleryTeam_Galleries_GalleriesId",
                        column: x => x.GalleriesId,
                        principalTable: "Galleries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GalleryTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TeamId",
                table: "Users",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_UserId",
                table: "Services",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_TeamId",
                table: "Organizations",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_UserId",
                table: "Organizations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ServiceId",
                table: "Galleries",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_GalleryTeam_TeamsId",
                table: "GalleryTeam",
                column: "TeamsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Galleries_GalleryId",
                table: "Fields",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Images_ThumbnailImageId",
                table: "Fields",
                column: "ThumbnailImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Locations_LocationId",
                table: "Fields",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Organizations_OrganizationId",
                table: "Fields",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Galleries_Services_ServiceId",
                table: "Galleries",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Fields_FieldId",
                table: "Games",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Galleries_GalleryId",
                table: "Images",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Galleries_GalleryId",
                table: "Listings",
                column: "GalleryId",
                principalTable: "Galleries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Listings_Locations_LocationId",
                table: "Listings",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Images_LogoImageId",
                table: "Organizations",
                column: "LogoImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Images_ThumbnailImageId",
                table: "Services",
                column: "ThumbnailImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Locations_LocationId",
                table: "Services",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Organizations_OrganizationId",
                table: "Services",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Users_UserId",
                table: "Services",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Images_LogoImageId",
                table: "Teams",
                column: "LogoImageId",
                principalTable: "Images",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Organizations_OrganizationId",
                table: "Teams",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Teams_TeamId",
                table: "Users",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
