using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace assnet8.Data.Configuration
{
    public class MunicipalityConfiguration : IEntityTypeConfiguration<Municipality>
    {
        public void Configure(EntityTypeBuilder<Municipality> builder)
        {
            builder.HasOne(m => m.Location)
                   .WithMany(l => l.Municipalities)
                   .HasForeignKey(m => m.LocationId)
                   .OnDelete(DeleteBehavior.Cascade);



            builder.HasData(
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111197"), LocationId = new Guid("11111111-1111-1111-1111-111111111197"), Name = "Aleksinac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111196"), LocationId = new Guid("11111111-1111-1111-1111-111111111196"), Name = "Aranđelovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111195"), LocationId = new Guid("11111111-1111-1111-1111-111111111195"), Name = "Aleksandrovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111194"), LocationId = new Guid("11111111-1111-1111-1111-111111111194"), Name = "Bajina Bašta" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111193"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Barajevo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111192"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Voždovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111191"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Vračar" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111189"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Grocka" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111188"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Zvezdara" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111187"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Zemun" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111186"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Lazarevac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111185"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Mladenovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111184"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Novi Beograd" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111183"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Obrenovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111182"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Palilula" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111181"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Rakovica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111179"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Savski Venac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111178"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Sopot" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111177"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Stari Grad" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111176"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Surčin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111175"), LocationId = new Guid("11111111-1111-1111-1111-111111111193"), Name = "Čukarica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111174"), LocationId = new Guid("11111111-1111-1111-1111-111111111192"), Name = "Majdanpek" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111173"), LocationId = new Guid("11111111-1111-1111-1111-111111111192"), Name = "Bor" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111172"), LocationId = new Guid("11111111-1111-1111-1111-111111111191"), Name = "Bačka Palanka" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111171"), LocationId = new Guid("11111111-1111-1111-1111-111111111189"), Name = "Bačka Topola" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111170"), LocationId = new Guid("11111111-1111-1111-1111-111111111188"), Name = "Bogatić" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111169"), LocationId = new Guid("11111111-1111-1111-1111-111111111187"), Name = "Bujanovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111168"), LocationId = new Guid("11111111-1111-1111-1111-111111111186"), Name = "Bečej" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111167"), LocationId = new Guid("11111111-1111-1111-1111-111111111185"), Name = "Valjevo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111166"), LocationId = new Guid("11111111-1111-1111-1111-111111111185"), Name = "Osečina" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111165"), LocationId = new Guid("11111111-1111-1111-1111-111111111185"), Name = "Mionica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111164"), LocationId = new Guid("11111111-1111-1111-1111-111111111185"), Name = "Ljig" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111163"), LocationId = new Guid("11111111-1111-1111-1111-111111111185"), Name = "Lajkovac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111162"), LocationId = new Guid("11111111-1111-1111-1111-111111111184"), Name = "Vrnjačka Banja" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111161"), LocationId = new Guid("11111111-1111-1111-1111-111111111183"), Name = "Vlasotince" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111160"), LocationId = new Guid("11111111-1111-1111-1111-111111111182"), Name = "Velika Plana" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111159"), LocationId = new Guid("11111111-1111-1111-1111-111111111181"), Name = "Bosilegrad" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111158"), LocationId = new Guid("11111111-1111-1111-1111-111111111181"), Name = "Vladičin Han" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111157"), LocationId = new Guid("11111111-1111-1111-1111-111111111181"), Name = "Preševo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111156"), LocationId = new Guid("11111111-1111-1111-1111-111111111181"), Name = "Trgovište" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111155"), LocationId = new Guid("11111111-1111-1111-1111-111111111181"), Name = "Vranje" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111154"), LocationId = new Guid("11111111-1111-1111-1111-111111111179"), Name = "Vrbas" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111153"), LocationId = new Guid("11111111-1111-1111-1111-111111111178"), Name = "Bela Crkva" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111152"), LocationId = new Guid("11111111-1111-1111-1111-111111111178"), Name = "Plandište" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111151"), LocationId = new Guid("11111111-1111-1111-1111-111111111178"), Name = "Vršac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111150"), LocationId = new Guid("11111111-1111-1111-1111-111111111177"), Name = "Vitina" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111149"), LocationId = new Guid("11111111-1111-1111-1111-111111111177"), Name = "Kosovska Kamenica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111148"), LocationId = new Guid("11111111-1111-1111-1111-111111111177"), Name = "Novo Brdo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111147"), LocationId = new Guid("11111111-1111-1111-1111-111111111177"), Name = "Gnjilane" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111146"), LocationId = new Guid("11111111-1111-1111-1111-111111111176"), Name = "Gornji Milanovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111145"), LocationId = new Guid("11111111-1111-1111-1111-111111111175"), Name = "Despotovac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111144"), LocationId = new Guid("11111111-1111-1111-1111-111111111174"), Name = "Dečani" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111143"), LocationId = new Guid("11111111-1111-1111-1111-111111111174"), Name = "Đakovica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111142"), LocationId = new Guid("11111111-1111-1111-1111-111111111173"), Name = "Boljevac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111141"), LocationId = new Guid("11111111-1111-1111-1111-111111111173"), Name = "Sokobanja" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111140"), LocationId = new Guid("11111111-1111-1111-1111-111111111173"), Name = "Zaječar" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111139"), LocationId = new Guid("11111111-1111-1111-1111-111111111172"), Name = "Žitište" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111138"), LocationId = new Guid("11111111-1111-1111-1111-111111111172"), Name = "Novi Bečej" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111137"), LocationId = new Guid("11111111-1111-1111-1111-111111111172"), Name = "Nova Crnja" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111136"), LocationId = new Guid("11111111-1111-1111-1111-111111111172"), Name = "Sečanj" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111135"), LocationId = new Guid("11111111-1111-1111-1111-111111111172"), Name = "Zrenjanin" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111134"), LocationId = new Guid("11111111-1111-1111-1111-111111111171"), Name = "Inđija" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111133"), LocationId = new Guid("11111111-1111-1111-1111-111111111170"), Name = "Ivanjica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111132"), LocationId = new Guid("11111111-1111-1111-1111-111111111169"), Name = "Rekovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111131"), LocationId = new Guid("11111111-1111-1111-1111-111111111169"), Name = "Jagodina" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111130"), LocationId = new Guid("11111111-1111-1111-1111-111111111168"), Name = "Kanjiža" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111129"), LocationId = new Guid("11111111-1111-1111-1111-111111111167"), Name = "Kraljevo" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111128"), LocationId = new Guid("11111111-1111-1111-1111-111111111166"), Name = "Batočina" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111127"), LocationId = new Guid("11111111-1111-1111-1111-111111111166"), Name = "Knić" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111126"), LocationId = new Guid("11111111-1111-1111-1111-111111111166"), Name = "Lapovo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111125"), LocationId = new Guid("11111111-1111-1111-1111-111111111166"), Name = "Rača" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111124"), LocationId = new Guid("11111111-1111-1111-1111-111111111166"), Name = "Kragujevac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111123"), LocationId = new Guid("11111111-1111-1111-1111-111111111165"), Name = "Knjaževac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111122"), LocationId = new Guid("11111111-1111-1111-1111-111111111164"), Name = "Čoka" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111121"), LocationId = new Guid("11111111-1111-1111-1111-111111111164"), Name = "Novi Kneževac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111120"), LocationId = new Guid("11111111-1111-1111-1111-111111111164"), Name = "Kikinda" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111119"), LocationId = new Guid("11111111-1111-1111-1111-111111111163"), Name = "Kladovo" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111118"), LocationId = new Guid("11111111-1111-1111-1111-111111111162"), Name = "Vučitrn" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111117"), LocationId = new Guid("11111111-1111-1111-1111-111111111162"), Name = "Zvečan" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111116"), LocationId = new Guid("11111111-1111-1111-1111-111111111162"), Name = "Zubin Potok" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111115"), LocationId = new Guid("11111111-1111-1111-1111-111111111162"), Name = "Leposavić" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111114"), LocationId = new Guid("11111111-1111-1111-1111-111111111162"), Name = "Srbica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111113"), LocationId = new Guid("11111111-1111-1111-1111-111111111162"), Name = "Kosovska Mitrovica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111112"), LocationId = new Guid("11111111-1111-1111-1111-111111111161"), Name = "Kovin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111111"), LocationId = new Guid("11111111-1111-1111-1111-111111111159"), Name = "Koceljeva" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111110"), LocationId = new Guid("11111111-1111-1111-1111-111111111158"), Name = "Brus" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111109"), LocationId = new Guid("11111111-1111-1111-1111-111111111158"), Name = "Varvarin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111108"), LocationId = new Guid("11111111-1111-1111-1111-111111111158"), Name = "Ćićevac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111107"), LocationId = new Guid("11111111-1111-1111-1111-111111111158"), Name = "Kruševac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111106"), LocationId = new Guid("11111111-1111-1111-1111-111111111157"), Name = "Lebane" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111105"), LocationId = new Guid("11111111-1111-1111-1111-111111111156"), Name = "Bojnik" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111104"), LocationId = new Guid("11111111-1111-1111-1111-111111111156"), Name = "Medveđa" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111103"), LocationId = new Guid("11111111-1111-1111-1111-111111111156"), Name = "Crna Trava" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111102"), LocationId = new Guid("11111111-1111-1111-1111-111111111156"), Name = "Leskovac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111101"), LocationId = new Guid("11111111-1111-1111-1111-111111111155"), Name = "Krupanj" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111100"), LocationId = new Guid("11111111-1111-1111-1111-111111111155"), Name = "Ljubovija" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111099"), LocationId = new Guid("11111111-1111-1111-1111-111111111155"), Name = "Mali Zvornik" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111098"), LocationId = new Guid("11111111-1111-1111-1111-111111111155"), Name = "Loznica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111097"), LocationId = new Guid("11111111-1111-1111-1111-111111111154"), Name = "Lučani" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111096"), LocationId = new Guid("11111111-1111-1111-1111-111111111153"), Name = "Nova Varoš" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111095"), LocationId = new Guid("11111111-1111-1111-1111-111111111152"), Name = "Negotin" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111094"), LocationId = new Guid("11111111-1111-1111-1111-111111111151"), Name = "Doljevac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111093"), LocationId = new Guid("11111111-1111-1111-1111-111111111151"), Name = "Gadžin Han" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111092"), LocationId = new Guid("11111111-1111-1111-1111-111111111151"), Name = "Merošina" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111091"), LocationId = new Guid("11111111-1111-1111-1111-111111111151"), Name = "Ražanj (mesto)" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111090"), LocationId = new Guid("11111111-1111-1111-1111-111111111151"), Name = "Svrljig" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111089"), LocationId = new Guid("11111111-1111-1111-1111-111111111151"), Name = "Niš" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111088"), LocationId = new Guid("11111111-1111-1111-1111-111111111149"), Name = "Tutin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111087"), LocationId = new Guid("11111111-1111-1111-1111-111111111149"), Name = "Novi Pazar" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111086"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Bač" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111085"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Bački Petrovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111084"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Beočin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111083"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Žabalj" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111082"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Srbobran" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111081"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Sremski Karlovci" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111080"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Temerin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111079"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Titel" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111078"), LocationId = new Guid("11111111-1111-1111-1111-111111111148"), Name = "Novi Sad" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111077"), LocationId = new Guid("11111111-1111-1111-1111-111111111147"), Name = "Alibunar" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111076"), LocationId = new Guid("11111111-1111-1111-1111-111111111147"), Name = "Kovačica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111075"), LocationId = new Guid("11111111-1111-1111-1111-111111111147"), Name = "Opovo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111074"), LocationId = new Guid("11111111-1111-1111-1111-111111111147"), Name = "Pančevo" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111073"), LocationId = new Guid("11111111-1111-1111-1111-111111111146"), Name = "Priboj" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111072"), LocationId = new Guid("11111111-1111-1111-1111-111111111145"), Name = "Istok" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111071"), LocationId = new Guid("11111111-1111-1111-1111-111111111145"), Name = "Klina" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111070"), LocationId = new Guid("11111111-1111-1111-1111-111111111145"), Name = "Peć" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111069"), LocationId = new Guid("11111111-1111-1111-1111-111111111144"), Name = "Požega" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111068"), LocationId = new Guid("11111111-1111-1111-1111-111111111143"), Name = "Gora" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111067"), LocationId = new Guid("11111111-1111-1111-1111-111111111143"), Name = "Suva Reka" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111066"), LocationId = new Guid("11111111-1111-1111-1111-111111111143"), Name = "Orahovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111065"), LocationId = new Guid("11111111-1111-1111-1111-111111111143"), Name = "Prizren" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111064"), LocationId = new Guid("11111111-1111-1111-1111-111111111142"), Name = "Babušnica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111063"), LocationId = new Guid("11111111-1111-1111-1111-111111111142"), Name = "Bela Palanka" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111062"), LocationId = new Guid("11111111-1111-1111-1111-111111111142"), Name = "Dimitrovgrad" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111061"), LocationId = new Guid("11111111-1111-1111-1111-111111111142"), Name = "Pirot" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111060"), LocationId = new Guid("11111111-1111-1111-1111-111111111141"), Name = "Blace" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111059"), LocationId = new Guid("11111111-1111-1111-1111-111111111141"), Name = "Žitorađa" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111058"), LocationId = new Guid("11111111-1111-1111-1111-111111111141"), Name = "Kuršumlija" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111057"), LocationId = new Guid("11111111-1111-1111-1111-111111111141"), Name = "Prokuplje" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111056"), LocationId = new Guid("11111111-1111-1111-1111-111111111139"), Name = "Paraćin" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111055"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Veliko Gradište" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111054"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Golubac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111053"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Žabari" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111052"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Žagubica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111051"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Kučevo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111050"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Malo Crniće" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111049"), LocationId = new Guid("11111111-1111-1111-1111-111111111138"), Name = "Požarevac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111048"), LocationId = new Guid("11111111-1111-1111-1111-111111111137"), Name = "Prijepolje" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111047"), LocationId = new Guid("11111111-1111-1111-1111-111111111136"), Name = "Podujevo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111046"), LocationId = new Guid("11111111-1111-1111-1111-111111111136"), Name = "Obilić" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111045"), LocationId = new Guid("11111111-1111-1111-1111-111111111136"), Name = "Priština" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111044"), LocationId = new Guid("11111111-1111-1111-1111-111111111136"), Name = "Lipljan" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111043"), LocationId = new Guid("11111111-1111-1111-1111-111111111136"), Name = "Kosovo Polje" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111042"), LocationId = new Guid("11111111-1111-1111-1111-111111111136"), Name = "Glogovac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111041"), LocationId = new Guid("11111111-1111-1111-1111-111111111135"), Name = "Petrovac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111100040"), LocationId = new Guid("11111111-1111-1111-1111-111111111134"), Name = "Raška" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111040"), LocationId = new Guid("11111111-1111-1111-1111-111111111133"), Name = "Irig" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111039"), LocationId = new Guid("11111111-1111-1111-1111-111111111133"), Name = "Pećinci" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111038"), LocationId = new Guid("11111111-1111-1111-1111-111111111133"), Name = "Ruma" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111037"), LocationId = new Guid("11111111-1111-1111-1111-111111111132"), Name = "Senta" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111036"), LocationId = new Guid("11111111-1111-1111-1111-111111111132"), Name = "Senta" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111035"), LocationId = new Guid("11111111-1111-1111-1111-111111111131"), Name = "Surdulica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111034"), LocationId = new Guid("11111111-1111-1111-1111-111111111129"), Name = "Svijalnac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111033"), LocationId = new Guid("11111111-1111-1111-1111-111111111128"), Name = "Smederevo" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111032"), LocationId = new Guid("11111111-1111-1111-1111-111111111127"), Name = "Sjenica" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111031"), LocationId = new Guid("11111111-1111-1111-1111-111111111126"), Name = "Sremska Mitrovica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111029"), LocationId = new Guid("11111111-1111-1111-1111-111111111125"), Name = "Apatin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111028"), LocationId = new Guid("11111111-1111-1111-1111-111111111125"), Name = "Kula" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111027"), LocationId = new Guid("11111111-1111-1111-1111-111111111125"), Name = "Odžaci" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111026"), LocationId = new Guid("11111111-1111-1111-1111-111111111125"), Name = "Sombor" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111025"), LocationId = new Guid("11111111-1111-1111-1111-111111111124"), Name = "Smederevska Palanka" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111024"), LocationId = new Guid("11111111-1111-1111-1111-111111111123"), Name = "Stara Pazova" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111023"), LocationId = new Guid("11111111-1111-1111-1111-111111111122"), Name = "Mali Iđoš" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111022"), LocationId = new Guid("11111111-1111-1111-1111-111111111122"), Name = "Subotica" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111021"), LocationId = new Guid("11111111-1111-1111-1111-111111111121"), Name = "Topola" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111020"), LocationId = new Guid("11111111-1111-1111-1111-111111111119"), Name = "Trstenik" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111019"), LocationId = new Guid("11111111-1111-1111-1111-111111111118"), Name = "Tutin" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111018"), LocationId = new Guid("11111111-1111-1111-1111-111111111117"), Name = "Ćuprija" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111017"), LocationId = new Guid("11111111-1111-1111-1111-111111111116"), Name = "Ub" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111016"), LocationId = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Arilje" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111015"), LocationId = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Kosjerić" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111014"), LocationId = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Čajetina" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111013"), LocationId = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Zlatibor" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111012"), LocationId = new Guid("11111111-1111-1111-1111-111111111115"), Name = "Užice" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111011"), LocationId = new Guid("11111111-1111-1111-1111-111111111114"), Name = "Štrpce" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111010"), LocationId = new Guid("11111111-1111-1111-1111-111111111114"), Name = "Štimlje" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111009"), LocationId = new Guid("11111111-1111-1111-1111-111111111114"), Name = "Uroševac" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111008"), LocationId = new Guid("11111111-1111-1111-1111-111111111114"), Name = "Kačanik" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111007"), LocationId = new Guid("11111111-1111-1111-1111-111111111113"), Name = "Čačak" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111006"), LocationId = new Guid("11111111-1111-1111-1111-111111111112"), Name = "Vladimirci" },
            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111005"), LocationId = new Guid("11111111-1111-1111-1111-111111111112"), Name = "Šabac" },

            new Municipality { Id = new Guid("11111111-1111-1111-1111-111111111004"), LocationId = new Guid("11111111-1111-1111-1111-111111111111"), Name = "Šid" }
            );
        }
    }
}