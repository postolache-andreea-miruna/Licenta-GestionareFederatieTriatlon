﻿// <auto-generated />
using System;
using GestionareFederatieTriatlon.Entitati;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionareFederatieTriatlon.Migrations
{
    [DbContext(typeof(GestionareFederatieTriatlonContext))]
    [Migration("20221216113931_ClubModificare")]
    partial class ClubModificare
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Chat", b =>
                {
                    b.Property<int>("codChat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codChat"));

                    b.Property<DateTime>("dataInfiintarii")
                        .HasColumnType("datetime2");

                    b.HasKey("codChat");

                    b.ToTable("Chaturi");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Club", b =>
                {
                    b.Property<int>("codClub")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codClub"));

                    b.Property<string>("descriere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codClub");

                    b.ToTable("Cluburi");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Comentariu", b =>
                {
                    b.Property<int>("codPostare")
                        .HasColumnType("int");

                    b.Property<int>("codComentariu")
                        .HasColumnType("int");

                    b.Property<string>("codUtilizatorComentariu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mesajComentariu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codPostare", "codComentariu");

                    b.ToTable("Comentarii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Competitie", b =>
                {
                    b.Property<int>("codCompetitie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codCompetitie"));

                    b.Property<int>("codLocatie")
                        .HasColumnType("int");

                    b.Property<int>("codTip")
                        .HasColumnType("int");

                    b.Property<DateTime>("dataFinal")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dataStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("numeCompetitie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("taxaParticipare")
                        .HasColumnType("real");

                    b.HasKey("codCompetitie");

                    b.HasIndex("codLocatie");

                    b.HasIndex("codTip");

                    b.ToTable("Competitii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Conversatie", b =>
                {
                    b.Property<int>("codConversatie")
                        .HasColumnType("int");

                    b.Property<string>("codUtilizator")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("codChat")
                        .HasColumnType("int");

                    b.Property<string>("codUtilizator2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("dataLivrareMesaj")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dataPrimireMesaj")
                        .HasColumnType("datetime2");

                    b.Property<string>("mesajConversatie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codConversatie", "codUtilizator", "codChat");

                    b.HasIndex("codChat");

                    b.HasIndex("codUtilizator");

                    b.ToTable("Conversatii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Formular", b =>
                {
                    b.Property<int>("codFormular")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codFormular"));

                    b.Property<string>("avizMedical")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("buletin_CertificatNastere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("codUtilizator")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("completareFormular")
                        .HasColumnType("datetime2");

                    b.Property<string>("pozaProfil")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codFormular");

                    b.HasIndex("codUtilizator");

                    b.ToTable("Fomulare");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Istoric", b =>
                {
                    b.Property<string>("codUtilizator")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("codProba")
                        .HasColumnType("int");

                    b.Property<int>("codCompetitie")
                        .HasColumnType("int");

                    b.Property<string>("categorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("loc")
                        .HasColumnType("int");

                    b.Property<int>("puncte")
                        .HasColumnType("int");

                    b.Property<int>("timp")
                        .HasColumnType("int");

                    b.HasKey("codUtilizator", "codProba", "codCompetitie");

                    b.HasIndex("codCompetitie");

                    b.HasIndex("codProba");

                    b.ToTable("Istorice");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Locatie", b =>
                {
                    b.Property<int>("codLocatie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codLocatie"));

                    b.Property<int>("numarStrada")
                        .HasColumnType("int");

                    b.Property<string>("oras")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("strada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tara")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codLocatie");

                    b.ToTable("Locatii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Notificare", b =>
                {
                    b.Property<int>("codNotificare")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codNotificare"));

                    b.Property<bool>("citireNotificare")
                        .HasColumnType("bit");

                    b.Property<string>("codUtilizator")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("codUtilizator2")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("dataCreare")
                        .HasColumnType("datetime2");

                    b.Property<string>("mesaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("titluNotificare")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codNotificare");

                    b.HasIndex("codUtilizator");

                    b.HasIndex("codUtilizator2");

                    b.ToTable("Notificari");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Postare", b =>
                {
                    b.Property<int>("codPostare")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codPostare"));

                    b.Property<string>("codUtilizator")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("dataPostare")
                        .HasColumnType("datetime2");

                    b.Property<int>("numarReactiiFericire")
                        .HasColumnType("int");

                    b.Property<int>("numarReactiiTristete")
                        .HasColumnType("int");

                    b.Property<string>("urlPoza")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codPostare");

                    b.HasIndex("codUtilizator");

                    b.ToTable("Postari");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Proba", b =>
                {
                    b.Property<int>("codProba")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codProba"));

                    b.Property<string>("numeProba")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("timpLimita")
                        .HasColumnType("int");

                    b.HasKey("codProba");

                    b.ToTable("Probe");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Recenzie", b =>
                {
                    b.Property<int>("codRecenzie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codRecenzie"));

                    b.Property<int>("codCompetitie")
                        .HasColumnType("int");

                    b.Property<string>("codUtilizator")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("completareRecenzie")
                        .HasColumnType("datetime2");

                    b.Property<int>("numarStele")
                        .HasColumnType("int");

                    b.Property<string>("text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codRecenzie");

                    b.HasIndex("codCompetitie");

                    b.HasIndex("codUtilizator");

                    b.ToTable("Recenzii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Rol", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.RoluriUtilizator", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Tip", b =>
                {
                    b.Property<int>("codTip")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codTip"));

                    b.Property<int>("numarMinimParticipanti")
                        .HasColumnType("int");

                    b.Property<string>("tipCompetitie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codTip");

                    b.ToTable("Tipuri");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Utilizator", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("abonareStiri")
                        .HasColumnType("bit");

                    b.Property<string>("codAntrenor")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("codClub")
                        .HasColumnType("int");

                    b.Property<string>("nume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prenume")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("urlPozaProfil")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("codAntrenor");

                    b.HasIndex("codClub");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("Utilizator");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Videoclip", b =>
                {
                    b.Property<int>("codVideo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("codVideo"));

                    b.Property<int>("codCompetitie")
                        .HasColumnType("int");

                    b.Property<string>("tipExtensie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("urlVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codVideo");

                    b.HasIndex("codCompetitie")
                        .IsUnique();

                    b.ToTable("Videoclipuri");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Antrenor", b =>
                {
                    b.HasBaseType("GestionareFederatieTriatlon.Entitati.Utilizator");

                    b.Property<string>("gradPregatire")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Antrenor");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Sportiv", b =>
                {
                    b.HasBaseType("GestionareFederatieTriatlon.Entitati.Utilizator");

                    b.Property<DateTime>("dataNastere")
                        .HasColumnType("datetime2");

                    b.Property<string>("gen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numarLegitimatie")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Sportiv");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Comentariu", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Postare", "Postare")
                        .WithMany("Comentarii")
                        .HasForeignKey("codPostare")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Postare");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Competitie", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Locatie", "Locatie")
                        .WithMany("Competitii")
                        .HasForeignKey("codLocatie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Tip", "Tip")
                        .WithMany("Competitii")
                        .HasForeignKey("codTip")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locatie");

                    b.Navigation("Tip");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Conversatie", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Chat", "Chat")
                        .WithMany("Conversatii")
                        .HasForeignKey("codChat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", "Utilizator")
                        .WithMany("Conversatii")
                        .HasForeignKey("codUtilizator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Utilizator");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Formular", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Sportiv", "Sportiv")
                        .WithMany("Formulare")
                        .HasForeignKey("codUtilizator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sportiv");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Istoric", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Competitie", "Competitie")
                        .WithMany("Istorice")
                        .HasForeignKey("codCompetitie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Proba", "Proba")
                        .WithMany("Istorice")
                        .HasForeignKey("codProba")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Sportiv", "Sportiv")
                        .WithMany("Istorice")
                        .HasForeignKey("codUtilizator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competitie");

                    b.Navigation("Proba");

                    b.Navigation("Sportiv");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Notificare", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", "Utilizator")
                        .WithMany("Notificari")
                        .HasForeignKey("codUtilizator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", "Utilizator2")
                        .WithMany()
                        .HasForeignKey("codUtilizator2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilizator");

                    b.Navigation("Utilizator2");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Postare", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", "Utilizator")
                        .WithMany("Postari")
                        .HasForeignKey("codUtilizator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Utilizator");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Recenzie", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Competitie", "Competitie")
                        .WithMany("Recenzii")
                        .HasForeignKey("codCompetitie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Sportiv", "Sportiv")
                        .WithMany("Recenzii")
                        .HasForeignKey("codUtilizator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competitie");

                    b.Navigation("Sportiv");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.RoluriUtilizator", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Rol", "Rol")
                        .WithMany("RoluriUtilizatori")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", "Utilizator")
                        .WithMany("RoluriUtilizatori")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Utilizator");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Utilizator", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", "AntrenorUtilizator")
                        .WithMany()
                        .HasForeignKey("codAntrenor");

                    b.HasOne("GestionareFederatieTriatlon.Entitati.Club", "Club")
                        .WithMany("Utilizatori")
                        .HasForeignKey("codClub")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AntrenorUtilizator");

                    b.Navigation("Club");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Videoclip", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Competitie", "Competitie")
                        .WithOne("Videoclip")
                        .HasForeignKey("GestionareFederatieTriatlon.Entitati.Videoclip", "codCompetitie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Competitie");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Rol", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GestionareFederatieTriatlon.Entitati.Utilizator", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Chat", b =>
                {
                    b.Navigation("Conversatii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Club", b =>
                {
                    b.Navigation("Utilizatori");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Competitie", b =>
                {
                    b.Navigation("Istorice");

                    b.Navigation("Recenzii");

                    b.Navigation("Videoclip")
                        .IsRequired();
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Locatie", b =>
                {
                    b.Navigation("Competitii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Postare", b =>
                {
                    b.Navigation("Comentarii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Proba", b =>
                {
                    b.Navigation("Istorice");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Rol", b =>
                {
                    b.Navigation("RoluriUtilizatori");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Tip", b =>
                {
                    b.Navigation("Competitii");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Utilizator", b =>
                {
                    b.Navigation("Conversatii");

                    b.Navigation("Notificari");

                    b.Navigation("Postari");

                    b.Navigation("RoluriUtilizatori");
                });

            modelBuilder.Entity("GestionareFederatieTriatlon.Entitati.Sportiv", b =>
                {
                    b.Navigation("Formulare");

                    b.Navigation("Istorice");

                    b.Navigation("Recenzii");
                });
#pragma warning restore 612, 618
        }
    }
}
