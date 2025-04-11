using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace assnet8.Migrations
{
    /// <inheritdoc />
    public partial class NewMunicipalities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "LocationId", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111100040"), new Guid("11111111-1111-1111-1111-111111111134"), "Raška" },
                    { new Guid("11111111-1111-1111-1111-111111111004"), new Guid("11111111-1111-1111-1111-111111111111"), "Šid" },
                    { new Guid("11111111-1111-1111-1111-111111111005"), new Guid("11111111-1111-1111-1111-111111111112"), "Šabac" },
                    { new Guid("11111111-1111-1111-1111-111111111006"), new Guid("11111111-1111-1111-1111-111111111112"), "Vladimirci" },
                    { new Guid("11111111-1111-1111-1111-111111111007"), new Guid("11111111-1111-1111-1111-111111111113"), "Čačak" },
                    { new Guid("11111111-1111-1111-1111-111111111008"), new Guid("11111111-1111-1111-1111-111111111114"), "Kačanik" },
                    { new Guid("11111111-1111-1111-1111-111111111009"), new Guid("11111111-1111-1111-1111-111111111114"), "Uroševac" },
                    { new Guid("11111111-1111-1111-1111-111111111010"), new Guid("11111111-1111-1111-1111-111111111114"), "Štimlje" },
                    { new Guid("11111111-1111-1111-1111-111111111011"), new Guid("11111111-1111-1111-1111-111111111114"), "Štrpce" },
                    { new Guid("11111111-1111-1111-1111-111111111012"), new Guid("11111111-1111-1111-1111-111111111115"), "Užice" },
                    { new Guid("11111111-1111-1111-1111-111111111013"), new Guid("11111111-1111-1111-1111-111111111115"), "Zlatibor" },
                    { new Guid("11111111-1111-1111-1111-111111111014"), new Guid("11111111-1111-1111-1111-111111111115"), "Čajetina" },
                    { new Guid("11111111-1111-1111-1111-111111111015"), new Guid("11111111-1111-1111-1111-111111111115"), "Kosjerić" },
                    { new Guid("11111111-1111-1111-1111-111111111016"), new Guid("11111111-1111-1111-1111-111111111115"), "Arilje" },
                    { new Guid("11111111-1111-1111-1111-111111111017"), new Guid("11111111-1111-1111-1111-111111111116"), "Ub" },
                    { new Guid("11111111-1111-1111-1111-111111111018"), new Guid("11111111-1111-1111-1111-111111111117"), "Ćuprija" },
                    { new Guid("11111111-1111-1111-1111-111111111019"), new Guid("11111111-1111-1111-1111-111111111118"), "Tutin" },
                    { new Guid("11111111-1111-1111-1111-111111111020"), new Guid("11111111-1111-1111-1111-111111111119"), "Trstenik" },
                    { new Guid("11111111-1111-1111-1111-111111111021"), new Guid("11111111-1111-1111-1111-111111111121"), "Topola" },
                    { new Guid("11111111-1111-1111-1111-111111111022"), new Guid("11111111-1111-1111-1111-111111111122"), "Subotica" },
                    { new Guid("11111111-1111-1111-1111-111111111023"), new Guid("11111111-1111-1111-1111-111111111122"), "Mali Iđoš" },
                    { new Guid("11111111-1111-1111-1111-111111111024"), new Guid("11111111-1111-1111-1111-111111111123"), "Stara Pazova" },
                    { new Guid("11111111-1111-1111-1111-111111111025"), new Guid("11111111-1111-1111-1111-111111111124"), "Smederevska Palanka" },
                    { new Guid("11111111-1111-1111-1111-111111111026"), new Guid("11111111-1111-1111-1111-111111111125"), "Sombor" },
                    { new Guid("11111111-1111-1111-1111-111111111027"), new Guid("11111111-1111-1111-1111-111111111125"), "Odžaci" },
                    { new Guid("11111111-1111-1111-1111-111111111028"), new Guid("11111111-1111-1111-1111-111111111125"), "Kula" },
                    { new Guid("11111111-1111-1111-1111-111111111029"), new Guid("11111111-1111-1111-1111-111111111125"), "Apatin" },
                    { new Guid("11111111-1111-1111-1111-111111111031"), new Guid("11111111-1111-1111-1111-111111111126"), "Sremska Mitrovica" },
                    { new Guid("11111111-1111-1111-1111-111111111032"), new Guid("11111111-1111-1111-1111-111111111127"), "Sjenica" },
                    { new Guid("11111111-1111-1111-1111-111111111033"), new Guid("11111111-1111-1111-1111-111111111128"), "Smederevo" },
                    { new Guid("11111111-1111-1111-1111-111111111034"), new Guid("11111111-1111-1111-1111-111111111129"), "Svijalnac" },
                    { new Guid("11111111-1111-1111-1111-111111111035"), new Guid("11111111-1111-1111-1111-111111111131"), "Surdulica" },
                    { new Guid("11111111-1111-1111-1111-111111111036"), new Guid("11111111-1111-1111-1111-111111111132"), "Senta" },
                    { new Guid("11111111-1111-1111-1111-111111111037"), new Guid("11111111-1111-1111-1111-111111111132"), "Senta" },
                    { new Guid("11111111-1111-1111-1111-111111111038"), new Guid("11111111-1111-1111-1111-111111111133"), "Ruma" },
                    { new Guid("11111111-1111-1111-1111-111111111039"), new Guid("11111111-1111-1111-1111-111111111133"), "Pećinci" },
                    { new Guid("11111111-1111-1111-1111-111111111040"), new Guid("11111111-1111-1111-1111-111111111133"), "Irig" },
                    { new Guid("11111111-1111-1111-1111-111111111041"), new Guid("11111111-1111-1111-1111-111111111135"), "Petrovac" },
                    { new Guid("11111111-1111-1111-1111-111111111042"), new Guid("11111111-1111-1111-1111-111111111136"), "Glogovac" },
                    { new Guid("11111111-1111-1111-1111-111111111043"), new Guid("11111111-1111-1111-1111-111111111136"), "Kosovo Polje" },
                    { new Guid("11111111-1111-1111-1111-111111111044"), new Guid("11111111-1111-1111-1111-111111111136"), "Lipljan" },
                    { new Guid("11111111-1111-1111-1111-111111111045"), new Guid("11111111-1111-1111-1111-111111111136"), "Priština" },
                    { new Guid("11111111-1111-1111-1111-111111111046"), new Guid("11111111-1111-1111-1111-111111111136"), "Obilić" },
                    { new Guid("11111111-1111-1111-1111-111111111047"), new Guid("11111111-1111-1111-1111-111111111136"), "Podujevo" },
                    { new Guid("11111111-1111-1111-1111-111111111048"), new Guid("11111111-1111-1111-1111-111111111137"), "Prijepolje" },
                    { new Guid("11111111-1111-1111-1111-111111111049"), new Guid("11111111-1111-1111-1111-111111111138"), "Požarevac" },
                    { new Guid("11111111-1111-1111-1111-111111111050"), new Guid("11111111-1111-1111-1111-111111111138"), "Malo Crniće" },
                    { new Guid("11111111-1111-1111-1111-111111111051"), new Guid("11111111-1111-1111-1111-111111111138"), "Kučevo" },
                    { new Guid("11111111-1111-1111-1111-111111111052"), new Guid("11111111-1111-1111-1111-111111111138"), "Žagubica" },
                    { new Guid("11111111-1111-1111-1111-111111111053"), new Guid("11111111-1111-1111-1111-111111111138"), "Žabari" },
                    { new Guid("11111111-1111-1111-1111-111111111054"), new Guid("11111111-1111-1111-1111-111111111138"), "Golubac" },
                    { new Guid("11111111-1111-1111-1111-111111111055"), new Guid("11111111-1111-1111-1111-111111111138"), "Veliko Gradište" },
                    { new Guid("11111111-1111-1111-1111-111111111056"), new Guid("11111111-1111-1111-1111-111111111139"), "Paraćin" },
                    { new Guid("11111111-1111-1111-1111-111111111057"), new Guid("11111111-1111-1111-1111-111111111141"), "Prokuplje" },
                    { new Guid("11111111-1111-1111-1111-111111111058"), new Guid("11111111-1111-1111-1111-111111111141"), "Kuršumlija" },
                    { new Guid("11111111-1111-1111-1111-111111111059"), new Guid("11111111-1111-1111-1111-111111111141"), "Žitorađa" },
                    { new Guid("11111111-1111-1111-1111-111111111060"), new Guid("11111111-1111-1111-1111-111111111141"), "Blace" },
                    { new Guid("11111111-1111-1111-1111-111111111061"), new Guid("11111111-1111-1111-1111-111111111142"), "Pirot" },
                    { new Guid("11111111-1111-1111-1111-111111111062"), new Guid("11111111-1111-1111-1111-111111111142"), "Dimitrovgrad" },
                    { new Guid("11111111-1111-1111-1111-111111111063"), new Guid("11111111-1111-1111-1111-111111111142"), "Bela Palanka" },
                    { new Guid("11111111-1111-1111-1111-111111111064"), new Guid("11111111-1111-1111-1111-111111111142"), "Babušnica" },
                    { new Guid("11111111-1111-1111-1111-111111111065"), new Guid("11111111-1111-1111-1111-111111111143"), "Prizren" },
                    { new Guid("11111111-1111-1111-1111-111111111066"), new Guid("11111111-1111-1111-1111-111111111143"), "Orahovac" },
                    { new Guid("11111111-1111-1111-1111-111111111067"), new Guid("11111111-1111-1111-1111-111111111143"), "Suva Reka" },
                    { new Guid("11111111-1111-1111-1111-111111111068"), new Guid("11111111-1111-1111-1111-111111111143"), "Gora" },
                    { new Guid("11111111-1111-1111-1111-111111111069"), new Guid("11111111-1111-1111-1111-111111111144"), "Požega" },
                    { new Guid("11111111-1111-1111-1111-111111111070"), new Guid("11111111-1111-1111-1111-111111111145"), "Peć" },
                    { new Guid("11111111-1111-1111-1111-111111111071"), new Guid("11111111-1111-1111-1111-111111111145"), "Klina" },
                    { new Guid("11111111-1111-1111-1111-111111111072"), new Guid("11111111-1111-1111-1111-111111111145"), "Istok" },
                    { new Guid("11111111-1111-1111-1111-111111111073"), new Guid("11111111-1111-1111-1111-111111111146"), "Priboj" },
                    { new Guid("11111111-1111-1111-1111-111111111074"), new Guid("11111111-1111-1111-1111-111111111147"), "Pančevo" },
                    { new Guid("11111111-1111-1111-1111-111111111075"), new Guid("11111111-1111-1111-1111-111111111147"), "Opovo" },
                    { new Guid("11111111-1111-1111-1111-111111111076"), new Guid("11111111-1111-1111-1111-111111111147"), "Kovačica" },
                    { new Guid("11111111-1111-1111-1111-111111111077"), new Guid("11111111-1111-1111-1111-111111111147"), "Alibunar" },
                    { new Guid("11111111-1111-1111-1111-111111111078"), new Guid("11111111-1111-1111-1111-111111111148"), "Novi Sad" },
                    { new Guid("11111111-1111-1111-1111-111111111079"), new Guid("11111111-1111-1111-1111-111111111148"), "Titel" },
                    { new Guid("11111111-1111-1111-1111-111111111080"), new Guid("11111111-1111-1111-1111-111111111148"), "Temerin" },
                    { new Guid("11111111-1111-1111-1111-111111111081"), new Guid("11111111-1111-1111-1111-111111111148"), "Sremski Karlovci" },
                    { new Guid("11111111-1111-1111-1111-111111111082"), new Guid("11111111-1111-1111-1111-111111111148"), "Srbobran" },
                    { new Guid("11111111-1111-1111-1111-111111111083"), new Guid("11111111-1111-1111-1111-111111111148"), "Žabalj" },
                    { new Guid("11111111-1111-1111-1111-111111111084"), new Guid("11111111-1111-1111-1111-111111111148"), "Beočin" },
                    { new Guid("11111111-1111-1111-1111-111111111085"), new Guid("11111111-1111-1111-1111-111111111148"), "Bački Petrovac" },
                    { new Guid("11111111-1111-1111-1111-111111111086"), new Guid("11111111-1111-1111-1111-111111111148"), "Bač" },
                    { new Guid("11111111-1111-1111-1111-111111111087"), new Guid("11111111-1111-1111-1111-111111111149"), "Novi Pazar" },
                    { new Guid("11111111-1111-1111-1111-111111111088"), new Guid("11111111-1111-1111-1111-111111111149"), "Tutin" },
                    { new Guid("11111111-1111-1111-1111-111111111089"), new Guid("11111111-1111-1111-1111-111111111151"), "Niš" },
                    { new Guid("11111111-1111-1111-1111-111111111090"), new Guid("11111111-1111-1111-1111-111111111151"), "Svrljig" },
                    { new Guid("11111111-1111-1111-1111-111111111091"), new Guid("11111111-1111-1111-1111-111111111151"), "Ražanj (mesto)" },
                    { new Guid("11111111-1111-1111-1111-111111111092"), new Guid("11111111-1111-1111-1111-111111111151"), "Merošina" },
                    { new Guid("11111111-1111-1111-1111-111111111093"), new Guid("11111111-1111-1111-1111-111111111151"), "Gadžin Han" },
                    { new Guid("11111111-1111-1111-1111-111111111094"), new Guid("11111111-1111-1111-1111-111111111151"), "Doljevac" },
                    { new Guid("11111111-1111-1111-1111-111111111095"), new Guid("11111111-1111-1111-1111-111111111152"), "Negotin" },
                    { new Guid("11111111-1111-1111-1111-111111111096"), new Guid("11111111-1111-1111-1111-111111111153"), "Nova Varoš" },
                    { new Guid("11111111-1111-1111-1111-111111111097"), new Guid("11111111-1111-1111-1111-111111111154"), "Lučani" },
                    { new Guid("11111111-1111-1111-1111-111111111098"), new Guid("11111111-1111-1111-1111-111111111155"), "Loznica" },
                    { new Guid("11111111-1111-1111-1111-111111111099"), new Guid("11111111-1111-1111-1111-111111111155"), "Mali Zvornik" },
                    { new Guid("11111111-1111-1111-1111-111111111100"), new Guid("11111111-1111-1111-1111-111111111155"), "Ljubovija" },
                    { new Guid("11111111-1111-1111-1111-111111111101"), new Guid("11111111-1111-1111-1111-111111111155"), "Krupanj" },
                    { new Guid("11111111-1111-1111-1111-111111111102"), new Guid("11111111-1111-1111-1111-111111111156"), "Leskovac" },
                    { new Guid("11111111-1111-1111-1111-111111111103"), new Guid("11111111-1111-1111-1111-111111111156"), "Crna Trava" },
                    { new Guid("11111111-1111-1111-1111-111111111104"), new Guid("11111111-1111-1111-1111-111111111156"), "Medveđa" },
                    { new Guid("11111111-1111-1111-1111-111111111105"), new Guid("11111111-1111-1111-1111-111111111156"), "Bojnik" },
                    { new Guid("11111111-1111-1111-1111-111111111106"), new Guid("11111111-1111-1111-1111-111111111157"), "Lebane" },
                    { new Guid("11111111-1111-1111-1111-111111111107"), new Guid("11111111-1111-1111-1111-111111111158"), "Kruševac" },
                    { new Guid("11111111-1111-1111-1111-111111111108"), new Guid("11111111-1111-1111-1111-111111111158"), "Ćićevac" },
                    { new Guid("11111111-1111-1111-1111-111111111109"), new Guid("11111111-1111-1111-1111-111111111158"), "Varvarin" },
                    { new Guid("11111111-1111-1111-1111-111111111110"), new Guid("11111111-1111-1111-1111-111111111158"), "Brus" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111159"), "Koceljeva" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new Guid("11111111-1111-1111-1111-111111111161"), "Kovin" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new Guid("11111111-1111-1111-1111-111111111162"), "Kosovska Mitrovica" },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new Guid("11111111-1111-1111-1111-111111111162"), "Srbica" },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new Guid("11111111-1111-1111-1111-111111111162"), "Leposavić" },
                    { new Guid("11111111-1111-1111-1111-111111111116"), new Guid("11111111-1111-1111-1111-111111111162"), "Zubin Potok" },
                    { new Guid("11111111-1111-1111-1111-111111111117"), new Guid("11111111-1111-1111-1111-111111111162"), "Zvečan" },
                    { new Guid("11111111-1111-1111-1111-111111111118"), new Guid("11111111-1111-1111-1111-111111111162"), "Vučitrn" },
                    { new Guid("11111111-1111-1111-1111-111111111119"), new Guid("11111111-1111-1111-1111-111111111163"), "Kladovo" },
                    { new Guid("11111111-1111-1111-1111-111111111120"), new Guid("11111111-1111-1111-1111-111111111164"), "Kikinda" },
                    { new Guid("11111111-1111-1111-1111-111111111121"), new Guid("11111111-1111-1111-1111-111111111164"), "Novi Kneževac" },
                    { new Guid("11111111-1111-1111-1111-111111111122"), new Guid("11111111-1111-1111-1111-111111111164"), "Čoka" },
                    { new Guid("11111111-1111-1111-1111-111111111123"), new Guid("11111111-1111-1111-1111-111111111165"), "Knjaževac" },
                    { new Guid("11111111-1111-1111-1111-111111111124"), new Guid("11111111-1111-1111-1111-111111111166"), "Kragujevac" },
                    { new Guid("11111111-1111-1111-1111-111111111125"), new Guid("11111111-1111-1111-1111-111111111166"), "Rača" },
                    { new Guid("11111111-1111-1111-1111-111111111126"), new Guid("11111111-1111-1111-1111-111111111166"), "Lapovo" },
                    { new Guid("11111111-1111-1111-1111-111111111127"), new Guid("11111111-1111-1111-1111-111111111166"), "Knić" },
                    { new Guid("11111111-1111-1111-1111-111111111128"), new Guid("11111111-1111-1111-1111-111111111166"), "Batočina" },
                    { new Guid("11111111-1111-1111-1111-111111111129"), new Guid("11111111-1111-1111-1111-111111111167"), "Kraljevo" },
                    { new Guid("11111111-1111-1111-1111-111111111130"), new Guid("11111111-1111-1111-1111-111111111168"), "Kanjiža" },
                    { new Guid("11111111-1111-1111-1111-111111111131"), new Guid("11111111-1111-1111-1111-111111111169"), "Jagodina" },
                    { new Guid("11111111-1111-1111-1111-111111111132"), new Guid("11111111-1111-1111-1111-111111111169"), "Rekovac" },
                    { new Guid("11111111-1111-1111-1111-111111111133"), new Guid("11111111-1111-1111-1111-111111111170"), "Ivanjica" },
                    { new Guid("11111111-1111-1111-1111-111111111134"), new Guid("11111111-1111-1111-1111-111111111171"), "Inđija" },
                    { new Guid("11111111-1111-1111-1111-111111111135"), new Guid("11111111-1111-1111-1111-111111111172"), "Zrenjanin" },
                    { new Guid("11111111-1111-1111-1111-111111111136"), new Guid("11111111-1111-1111-1111-111111111172"), "Sečanj" },
                    { new Guid("11111111-1111-1111-1111-111111111137"), new Guid("11111111-1111-1111-1111-111111111172"), "Nova Crnja" },
                    { new Guid("11111111-1111-1111-1111-111111111138"), new Guid("11111111-1111-1111-1111-111111111172"), "Novi Bečej" },
                    { new Guid("11111111-1111-1111-1111-111111111139"), new Guid("11111111-1111-1111-1111-111111111172"), "Žitište" },
                    { new Guid("11111111-1111-1111-1111-111111111140"), new Guid("11111111-1111-1111-1111-111111111173"), "Zaječar" },
                    { new Guid("11111111-1111-1111-1111-111111111141"), new Guid("11111111-1111-1111-1111-111111111173"), "Sokobanja" },
                    { new Guid("11111111-1111-1111-1111-111111111142"), new Guid("11111111-1111-1111-1111-111111111173"), "Boljevac" },
                    { new Guid("11111111-1111-1111-1111-111111111143"), new Guid("11111111-1111-1111-1111-111111111174"), "Đakovica" },
                    { new Guid("11111111-1111-1111-1111-111111111144"), new Guid("11111111-1111-1111-1111-111111111174"), "Dečani" },
                    { new Guid("11111111-1111-1111-1111-111111111145"), new Guid("11111111-1111-1111-1111-111111111175"), "Despotovac" },
                    { new Guid("11111111-1111-1111-1111-111111111146"), new Guid("11111111-1111-1111-1111-111111111176"), "Gornji Milanovac" },
                    { new Guid("11111111-1111-1111-1111-111111111147"), new Guid("11111111-1111-1111-1111-111111111177"), "Gnjilane" },
                    { new Guid("11111111-1111-1111-1111-111111111148"), new Guid("11111111-1111-1111-1111-111111111177"), "Novo Brdo" },
                    { new Guid("11111111-1111-1111-1111-111111111149"), new Guid("11111111-1111-1111-1111-111111111177"), "Kosovska Kamenica" },
                    { new Guid("11111111-1111-1111-1111-111111111150"), new Guid("11111111-1111-1111-1111-111111111177"), "Vitina" },
                    { new Guid("11111111-1111-1111-1111-111111111151"), new Guid("11111111-1111-1111-1111-111111111178"), "Vršac" },
                    { new Guid("11111111-1111-1111-1111-111111111152"), new Guid("11111111-1111-1111-1111-111111111178"), "Plandište" },
                    { new Guid("11111111-1111-1111-1111-111111111153"), new Guid("11111111-1111-1111-1111-111111111178"), "Bela Crkva" },
                    { new Guid("11111111-1111-1111-1111-111111111154"), new Guid("11111111-1111-1111-1111-111111111179"), "Vrbas" },
                    { new Guid("11111111-1111-1111-1111-111111111155"), new Guid("11111111-1111-1111-1111-111111111181"), "Vranje" },
                    { new Guid("11111111-1111-1111-1111-111111111156"), new Guid("11111111-1111-1111-1111-111111111181"), "Trgovište" },
                    { new Guid("11111111-1111-1111-1111-111111111157"), new Guid("11111111-1111-1111-1111-111111111181"), "Preševo" },
                    { new Guid("11111111-1111-1111-1111-111111111158"), new Guid("11111111-1111-1111-1111-111111111181"), "Vladičin Han" },
                    { new Guid("11111111-1111-1111-1111-111111111159"), new Guid("11111111-1111-1111-1111-111111111181"), "Bosilegrad" },
                    { new Guid("11111111-1111-1111-1111-111111111160"), new Guid("11111111-1111-1111-1111-111111111182"), "Velika Plana" },
                    { new Guid("11111111-1111-1111-1111-111111111161"), new Guid("11111111-1111-1111-1111-111111111183"), "Vlasotince" },
                    { new Guid("11111111-1111-1111-1111-111111111162"), new Guid("11111111-1111-1111-1111-111111111184"), "Vrnjačka Banja" },
                    { new Guid("11111111-1111-1111-1111-111111111163"), new Guid("11111111-1111-1111-1111-111111111185"), "Lajkovac" },
                    { new Guid("11111111-1111-1111-1111-111111111164"), new Guid("11111111-1111-1111-1111-111111111185"), "Ljig" },
                    { new Guid("11111111-1111-1111-1111-111111111165"), new Guid("11111111-1111-1111-1111-111111111185"), "Mionica" },
                    { new Guid("11111111-1111-1111-1111-111111111166"), new Guid("11111111-1111-1111-1111-111111111185"), "Osečina" },
                    { new Guid("11111111-1111-1111-1111-111111111167"), new Guid("11111111-1111-1111-1111-111111111185"), "Valjevo" },
                    { new Guid("11111111-1111-1111-1111-111111111168"), new Guid("11111111-1111-1111-1111-111111111186"), "Bečej" },
                    { new Guid("11111111-1111-1111-1111-111111111169"), new Guid("11111111-1111-1111-1111-111111111187"), "Bujanovac" },
                    { new Guid("11111111-1111-1111-1111-111111111170"), new Guid("11111111-1111-1111-1111-111111111188"), "Bogatić" },
                    { new Guid("11111111-1111-1111-1111-111111111171"), new Guid("11111111-1111-1111-1111-111111111189"), "Bačka Topola" },
                    { new Guid("11111111-1111-1111-1111-111111111172"), new Guid("11111111-1111-1111-1111-111111111191"), "Bačka Palanka" },
                    { new Guid("11111111-1111-1111-1111-111111111173"), new Guid("11111111-1111-1111-1111-111111111192"), "Bor" },
                    { new Guid("11111111-1111-1111-1111-111111111174"), new Guid("11111111-1111-1111-1111-111111111192"), "Majdanpek" },
                    { new Guid("11111111-1111-1111-1111-111111111175"), new Guid("11111111-1111-1111-1111-111111111193"), "Čukarica" },
                    { new Guid("11111111-1111-1111-1111-111111111176"), new Guid("11111111-1111-1111-1111-111111111193"), "Surčin" },
                    { new Guid("11111111-1111-1111-1111-111111111177"), new Guid("11111111-1111-1111-1111-111111111193"), "Stari Grad" },
                    { new Guid("11111111-1111-1111-1111-111111111178"), new Guid("11111111-1111-1111-1111-111111111193"), "Sopot" },
                    { new Guid("11111111-1111-1111-1111-111111111179"), new Guid("11111111-1111-1111-1111-111111111193"), "Savski Venac" },
                    { new Guid("11111111-1111-1111-1111-111111111181"), new Guid("11111111-1111-1111-1111-111111111193"), "Rakovica" },
                    { new Guid("11111111-1111-1111-1111-111111111182"), new Guid("11111111-1111-1111-1111-111111111193"), "Palilula" },
                    { new Guid("11111111-1111-1111-1111-111111111183"), new Guid("11111111-1111-1111-1111-111111111193"), "Obrenovac" },
                    { new Guid("11111111-1111-1111-1111-111111111184"), new Guid("11111111-1111-1111-1111-111111111193"), "Novi Beograd" },
                    { new Guid("11111111-1111-1111-1111-111111111185"), new Guid("11111111-1111-1111-1111-111111111193"), "Mladenovac" },
                    { new Guid("11111111-1111-1111-1111-111111111186"), new Guid("11111111-1111-1111-1111-111111111193"), "Lazarevac" },
                    { new Guid("11111111-1111-1111-1111-111111111187"), new Guid("11111111-1111-1111-1111-111111111193"), "Zemun" },
                    { new Guid("11111111-1111-1111-1111-111111111188"), new Guid("11111111-1111-1111-1111-111111111193"), "Zvezdara" },
                    { new Guid("11111111-1111-1111-1111-111111111189"), new Guid("11111111-1111-1111-1111-111111111193"), "Grocka" },
                    { new Guid("11111111-1111-1111-1111-111111111191"), new Guid("11111111-1111-1111-1111-111111111193"), "Vračar" },
                    { new Guid("11111111-1111-1111-1111-111111111192"), new Guid("11111111-1111-1111-1111-111111111193"), "Voždovac" },
                    { new Guid("11111111-1111-1111-1111-111111111193"), new Guid("11111111-1111-1111-1111-111111111193"), "Barajevo" },
                    { new Guid("11111111-1111-1111-1111-111111111194"), new Guid("11111111-1111-1111-1111-111111111194"), "Bajina Bašta" },
                    { new Guid("11111111-1111-1111-1111-111111111195"), new Guid("11111111-1111-1111-1111-111111111195"), "Aleksandrovac" },
                    { new Guid("11111111-1111-1111-1111-111111111196"), new Guid("11111111-1111-1111-1111-111111111196"), "Aranđelovac" },
                    { new Guid("11111111-1111-1111-1111-111111111197"), new Guid("11111111-1111-1111-1111-111111111197"), "Aleksinac" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111100040"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111004"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111005"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111006"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111007"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111008"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111009"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111010"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111011"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111012"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111013"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111014"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111015"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111016"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111017"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111018"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111019"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111020"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111021"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111022"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111023"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111024"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111025"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111026"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111027"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111028"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111029"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111031"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111032"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111033"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111034"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111035"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111036"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111037"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111038"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111039"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111040"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111041"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111042"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111043"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111044"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111045"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111046"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111047"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111048"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111049"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111050"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111051"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111052"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111053"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111054"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111055"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111056"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111057"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111058"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111059"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111060"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111061"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111062"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111063"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111064"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111065"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111066"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111067"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111068"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111069"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111070"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111071"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111072"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111073"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111074"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111075"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111076"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111077"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111078"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111079"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111080"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111081"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111082"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111083"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111084"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111085"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111086"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111087"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111088"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111089"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111090"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111091"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111092"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111093"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111094"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111095"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111096"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111097"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111098"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111099"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111100"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111101"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111102"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111103"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111104"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111105"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111106"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111107"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111108"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111109"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111110"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111120"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111121"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111122"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111123"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111124"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111125"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111126"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111127"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111128"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111129"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111130"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111131"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111132"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111133"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111134"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111135"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111136"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111137"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111138"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111139"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111140"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111141"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111142"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111143"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111144"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111145"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111146"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111147"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111148"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111149"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111150"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111151"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111152"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111153"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111154"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111155"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111156"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111157"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111158"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111159"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111160"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111161"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111162"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111163"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111164"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111165"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111166"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111167"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111168"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111169"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111170"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111171"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111172"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111173"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111174"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111175"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111176"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111177"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111178"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111179"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111181"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111182"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111183"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111184"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111185"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111186"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111187"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111188"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111189"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111191"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111192"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111193"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111194"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111195"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111196"));

            migrationBuilder.DeleteData(
                table: "Municipalities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111197"));
        }
    }
}
