using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration;
public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Region)
            .IsRequired();

        builder.HasIndex(r => r.Region)
            .IsUnique();

        builder.Property(r => r.Registration)
            .IsRequired();

        builder.HasIndex(r => r.Registration)
            .IsUnique();

        builder.HasData(
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111197"), Region = "Aleksinac", Registration = "AL" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111196"), Region = "Aranđelovac", Registration = "AR" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111195"), Region = "Aleksandrovac", Registration = "AC" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111194"), Region = "Bajina Bašta", Registration = "BB" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111193"), Region = "Beograd", Registration = "BG" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111192"), Region = "Bor", Registration = "BO" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111191"), Region = "Bačka Palanka", Registration = "BP" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111189"), Region = "Bačka Topola", Registration = "BT" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111188"), Region = "Bogatić", Registration = "BĆ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111187"), Region = "Bujanovac", Registration = "BU" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111186"), Region = "Bečej", Registration = "BČ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111185"), Region = "Valjevo", Registration = "VA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111184"), Region = "Vrnjačka Banja", Registration = "VB" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111183"), Region = "Vlasotince", Registration = "VL" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111182"), Region = "Velika Plana", Registration = "VP" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111181"), Region = "Vranje", Registration = "VR" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111179"), Region = "Vrbas", Registration = "VS" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111178"), Region = "Vršac", Registration = "VŠ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111177"), Region = "Gnjilane", Registration = "GL" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111176"), Region = "Gornji Milanovac", Registration = "GM" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111175"), Region = "Despotovac", Registration = "DE" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111174"), Region = "Đakovica", Registration = "ĐA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111173"), Region = "Zaječar", Registration = "ZA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111172"), Region = "Zrenjanin", Registration = "ZR" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111171"), Region = "Inđija", Registration = "IN" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111170"), Region = "Ivanjica", Registration = "IC" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111169"), Region = "Jagodina", Registration = "JA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111168"), Region = "Kanjiža", Registration = "KA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111167"), Region = "Kraljevo", Registration = "KV" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111166"), Region = "Kragujevac", Registration = "KG" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111165"), Region = "Knjaževac", Registration = "KŽ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111164"), Region = "Kikinda", Registration = "KI" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111163"), Region = "Kladovo", Registration = "KL" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111162"), Region = "Kosovska Mitrovica", Registration = "KM" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111161"), Region = "Kovin", Registration = "KO" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111159"), Region = "Koceljeva", Registration = "KC" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111158"), Region = "Kruševac", Registration = "KŠ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111157"), Region = "Lebane", Registration = "LB" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111156"), Region = "Leskovac", Registration = "LE" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111155"), Region = "Loznica", Registration = "LO" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111154"), Region = "Lučani", Registration = "LU" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111153"), Region = "Nova Varoš", Registration = "NV" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111152"), Region = "Negotin", Registration = "NG" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111151"), Region = "Niš", Registration = "NI" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111149"), Region = "Novi Pazar", Registration = "NP" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111148"), Region = "Novi Sad", Registration = "NS" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111147"), Region = "Pančevo", Registration = "PA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111146"), Region = "Priboj", Registration = "PB" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111145"), Region = "Peć", Registration = "PE" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111144"), Region = "Požega", Registration = "PŽ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111143"), Region = "Prizren", Registration = "PZ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111142"), Region = "Pirot", Registration = "PI" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111141"), Region = "Prokuplje", Registration = "PK" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111139"), Region = "Paraćin", Registration = "PN" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111138"), Region = "Požarevac", Registration = "PO" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111137"), Region = "Prijepolje", Registration = "PP" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111136"), Region = "Priština", Registration = "PR" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111135"), Region = "Petrovac", Registration = "PT" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111134"), Region = "Raška", Registration = "RA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111133"), Region = "Ruma", Registration = "RU" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111132"), Region = "Senta", Registration = "SA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111131"), Region = "Surdulica", Registration = "SC" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111129"), Region = "Svijalnac", Registration = "SV" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111128"), Region = "Smederevo", Registration = "SD" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111127"), Region = "Sjenica", Registration = "SJ" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111126"), Region = "Sremska Mitrovica", Registration = "SM" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111125"), Region = "Sombor", Registration = "SO" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111124"), Region = "Smederevska Palanka", Registration = "SP" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111123"), Region = "Stara Pazova", Registration = "ST" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111122"), Region = "Subotica", Registration = "SU" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111121"), Region = "Topola", Registration = "TO" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111119"), Region = "Trstenik", Registration = "TS" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111118"), Region = "Tutin", Registration = "TT" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111117"), Region = "Ćuprija", Registration = "ĆU" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111116"), Region = "Ub", Registration = "UB" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111115"), Region = "Užice", Registration = "UE" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111114"), Region = "Uroševac", Registration = "UR" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111113"), Region = "Čačak", Registration = "ČA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111112"), Region = "Šabac", Registration = "ŠA" },
        new Location { Id = new Guid("11111111-1111-1111-1111-111111111111"), Region = "Šid", Registration = "ŠI" }
        );
    }
}