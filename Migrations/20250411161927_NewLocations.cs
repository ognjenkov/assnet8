using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace assnet8.Migrations;

/// <inheritdoc />
public partial class NewLocations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Registration",
            table: "Locations",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.AlterColumn<string>(
            name: "Region",
            table: "Locations",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(max)");

        migrationBuilder.InsertData(
            table: "Locations",
            columns: new[] { "Id", "Region", "Registration" },
            values: new object[,]
            {
                { new Guid("11111111-1111-1111-1111-111111111111"), "Šid", "ŠI" },
                { new Guid("11111111-1111-1111-1111-111111111112"), "Šabac", "ŠA" },
                { new Guid("11111111-1111-1111-1111-111111111113"), "Čačak", "ČA" },
                { new Guid("11111111-1111-1111-1111-111111111114"), "Uroševac", "UR" },
                { new Guid("11111111-1111-1111-1111-111111111115"), "Užice", "UE" },
                { new Guid("11111111-1111-1111-1111-111111111116"), "Ub", "UB" },
                { new Guid("11111111-1111-1111-1111-111111111117"), "Ćuprija", "ĆU" },
                { new Guid("11111111-1111-1111-1111-111111111118"), "Tutin", "TT" },
                { new Guid("11111111-1111-1111-1111-111111111119"), "Trstenik", "TS" },
                { new Guid("11111111-1111-1111-1111-111111111121"), "Topola", "TO" },
                { new Guid("11111111-1111-1111-1111-111111111122"), "Subotica", "SU" },
                { new Guid("11111111-1111-1111-1111-111111111123"), "Stara Pazova", "ST" },
                { new Guid("11111111-1111-1111-1111-111111111124"), "Smederevska Palanka", "SP" },
                { new Guid("11111111-1111-1111-1111-111111111125"), "Sombor", "SO" },
                { new Guid("11111111-1111-1111-1111-111111111126"), "Sremska Mitrovica", "SM" },
                { new Guid("11111111-1111-1111-1111-111111111127"), "Sjenica", "SJ" },
                { new Guid("11111111-1111-1111-1111-111111111128"), "Smederevo", "SD" },
                { new Guid("11111111-1111-1111-1111-111111111129"), "Svijalnac", "SV" },
                { new Guid("11111111-1111-1111-1111-111111111131"), "Surdulica", "SC" },
                { new Guid("11111111-1111-1111-1111-111111111132"), "Senta", "SA" },
                { new Guid("11111111-1111-1111-1111-111111111133"), "Ruma", "RU" },
                { new Guid("11111111-1111-1111-1111-111111111134"), "Raška", "RA" },
                { new Guid("11111111-1111-1111-1111-111111111135"), "Petrovac", "PT" },
                { new Guid("11111111-1111-1111-1111-111111111136"), "Priština", "PR" },
                { new Guid("11111111-1111-1111-1111-111111111137"), "Prijepolje", "PP" },
                { new Guid("11111111-1111-1111-1111-111111111138"), "Požarevac", "PO" },
                { new Guid("11111111-1111-1111-1111-111111111139"), "Paraćin", "PN" },
                { new Guid("11111111-1111-1111-1111-111111111141"), "Prokuplje", "PK" },
                { new Guid("11111111-1111-1111-1111-111111111142"), "Pirot", "PI" },
                { new Guid("11111111-1111-1111-1111-111111111143"), "Prizren", "PZ" },
                { new Guid("11111111-1111-1111-1111-111111111144"), "Požega", "PŽ" },
                { new Guid("11111111-1111-1111-1111-111111111145"), "Peć", "PE" },
                { new Guid("11111111-1111-1111-1111-111111111146"), "Priboj", "PB" },
                { new Guid("11111111-1111-1111-1111-111111111147"), "Pančevo", "PA" },
                { new Guid("11111111-1111-1111-1111-111111111148"), "Novi Sad", "NS" },
                { new Guid("11111111-1111-1111-1111-111111111149"), "Novi Pazar", "NP" },
                { new Guid("11111111-1111-1111-1111-111111111151"), "Niš", "NI" },
                { new Guid("11111111-1111-1111-1111-111111111152"), "Negotin", "NG" },
                { new Guid("11111111-1111-1111-1111-111111111153"), "Nova Varoš", "NV" },
                { new Guid("11111111-1111-1111-1111-111111111154"), "Lučani", "LU" },
                { new Guid("11111111-1111-1111-1111-111111111155"), "Loznica", "LO" },
                { new Guid("11111111-1111-1111-1111-111111111156"), "Leskovac", "LE" },
                { new Guid("11111111-1111-1111-1111-111111111157"), "Lebane", "LB" },
                { new Guid("11111111-1111-1111-1111-111111111158"), "Kruševac", "KŠ" },
                { new Guid("11111111-1111-1111-1111-111111111159"), "Koceljeva", "KC" },
                { new Guid("11111111-1111-1111-1111-111111111161"), "Kovin", "KO" },
                { new Guid("11111111-1111-1111-1111-111111111162"), "Kosovska Mitrovica", "KM" },
                { new Guid("11111111-1111-1111-1111-111111111163"), "Kladovo", "KL" },
                { new Guid("11111111-1111-1111-1111-111111111164"), "Kikinda", "KI" },
                { new Guid("11111111-1111-1111-1111-111111111165"), "Knjaževac", "KŽ" },
                { new Guid("11111111-1111-1111-1111-111111111166"), "Kragujevac", "KG" },
                { new Guid("11111111-1111-1111-1111-111111111167"), "Kraljevo", "KV" },
                { new Guid("11111111-1111-1111-1111-111111111168"), "Kanjiža", "KA" },
                { new Guid("11111111-1111-1111-1111-111111111169"), "Jagodina", "JA" },
                { new Guid("11111111-1111-1111-1111-111111111170"), "Ivanjica", "IC" },
                { new Guid("11111111-1111-1111-1111-111111111171"), "Inđija", "IN" },
                { new Guid("11111111-1111-1111-1111-111111111172"), "Zrenjanin", "ZR" },
                { new Guid("11111111-1111-1111-1111-111111111173"), "Zaječar", "ZA" },
                { new Guid("11111111-1111-1111-1111-111111111174"), "Đakovica", "ĐA" },
                { new Guid("11111111-1111-1111-1111-111111111175"), "Despotovac", "DE" },
                { new Guid("11111111-1111-1111-1111-111111111176"), "Gornji Milanovac", "GM" },
                { new Guid("11111111-1111-1111-1111-111111111177"), "Gnjilane", "GL" },
                { new Guid("11111111-1111-1111-1111-111111111178"), "Vršac", "VŠ" },
                { new Guid("11111111-1111-1111-1111-111111111179"), "Vrbas", "VS" },
                { new Guid("11111111-1111-1111-1111-111111111181"), "Vranje", "VR" },
                { new Guid("11111111-1111-1111-1111-111111111182"), "Velika Plana", "VP" },
                { new Guid("11111111-1111-1111-1111-111111111183"), "Vlasotince", "VL" },
                { new Guid("11111111-1111-1111-1111-111111111184"), "Vrnjačka Banja", "VB" },
                { new Guid("11111111-1111-1111-1111-111111111185"), "Valjevo", "VA" },
                { new Guid("11111111-1111-1111-1111-111111111186"), "Bečej", "BČ" },
                { new Guid("11111111-1111-1111-1111-111111111187"), "Bujanovac", "BU" },
                { new Guid("11111111-1111-1111-1111-111111111188"), "Bogatić", "BĆ" },
                { new Guid("11111111-1111-1111-1111-111111111189"), "Bačka Topola", "BT" },
                { new Guid("11111111-1111-1111-1111-111111111191"), "Bačka Palanka", "BP" },
                { new Guid("11111111-1111-1111-1111-111111111192"), "Bor", "BO" },
                { new Guid("11111111-1111-1111-1111-111111111193"), "Beograd", "BG" },
                { new Guid("11111111-1111-1111-1111-111111111194"), "Bajina Bašta", "BB" },
                { new Guid("11111111-1111-1111-1111-111111111195"), "Aleksandrovac", "AC" },
                { new Guid("11111111-1111-1111-1111-111111111196"), "Aranđelovac", "AR" },
                { new Guid("11111111-1111-1111-1111-111111111197"), "Aleksinac", "AL" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Locations_Region",
            table: "Locations",
            column: "Region",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Locations_Registration",
            table: "Locations",
            column: "Registration",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Locations_Region",
            table: "Locations");

        migrationBuilder.DropIndex(
            name: "IX_Locations_Registration",
            table: "Locations");

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111113"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111114"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111115"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111116"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111117"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111118"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111119"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111121"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111122"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111123"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111124"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111125"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111126"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111127"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111128"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111129"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111131"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111132"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111133"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111134"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111135"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111136"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111137"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111138"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111139"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111141"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111142"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111143"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111144"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111145"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111146"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111147"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111148"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111149"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111151"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111152"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111153"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111154"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111155"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111156"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111157"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111158"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111159"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111161"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111162"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111163"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111164"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111165"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111166"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111167"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111168"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111169"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111170"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111171"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111172"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111173"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111174"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111175"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111176"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111177"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111178"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111179"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111181"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111182"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111183"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111184"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111185"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111186"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111187"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111188"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111189"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111191"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111192"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111193"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111194"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111195"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111196"));

        migrationBuilder.DeleteData(
            table: "Locations",
            keyColumn: "Id",
            keyValue: new Guid("11111111-1111-1111-1111-111111111197"));

        migrationBuilder.AlterColumn<string>(
            name: "Registration",
            table: "Locations",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");

        migrationBuilder.AlterColumn<string>(
            name: "Region",
            table: "Locations",
            type: "nvarchar(max)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
    }
}