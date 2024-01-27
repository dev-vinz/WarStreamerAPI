﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarStreamer.Models.Context;

#nullable disable

namespace WarStreamer.Models.Migrations
{
    [DbContext(typeof(WarStreamerContext))]
    [Migration("20240127220341_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CS_AS")
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WarStreamer.Models.Account", b =>
                {
                    b.Property<string>("Tag")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Tag");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("WarStreamer.Models.AuthToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessIV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccessToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscordIV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscordToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("ExpiresAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("IssuedAt")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("UserId");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("WarStreamer.Models.Font", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Fonts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("26082a0f-5eb1-481d-be36-2056dd9d5dcd"),
                            DisplayName = "Clash of Clans",
                            FileName = "supercell-magic.ttf"
                        },
                        new
                        {
                            Id = new Guid("76c29079-e36e-4b21-a54a-f1136c3dade3"),
                            DisplayName = "Poppins",
                            FileName = "poppins.otf"
                        },
                        new
                        {
                            Id = new Guid("90278b54-85f7-4d1a-8eb1-e04b8d8c5bef"),
                            DisplayName = "Quicksand",
                            FileName = "quicksand.otf"
                        },
                        new
                        {
                            Id = new Guid("279ba36e-9913-4f76-b54f-47b3c49f2661"),
                            DisplayName = "Roboto",
                            FileName = "roboto.ttf"
                        });
                });

            modelBuilder.Entity("WarStreamer.Models.Image", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("bit");

                    b.Property<int>("LocationX")
                        .HasColumnType("int");

                    b.Property<int>("LocationY")
                        .HasColumnType("int");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("UserId", "Name");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WarStreamer.Models.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CultureInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FlagEmoji")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortcutValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");

                    b.HasData(
                        new
                        {
                            Id = new Guid("477eb317-2026-485a-9de6-f1b31b44b337"),
                            CultureInfo = "en-US",
                            DisplayValue = "English",
                            FlagEmoji = "🇬🇧",
                            ShortcutValue = "en"
                        },
                        new
                        {
                            Id = new Guid("65a350b4-7ec2-4fc6-88b0-f8cb36a39bea"),
                            CultureInfo = "fr-FR",
                            DisplayValue = "Français",
                            FlagEmoji = "🇫🇷",
                            ShortcutValue = "fr"
                        });
                });

            modelBuilder.Entity("WarStreamer.Models.OverlaySetting", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<int?>("AverageDurationLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("AverageDurationLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("AverageDurationSize")
                        .HasColumnType("int");

                    b.Property<int?>("ClanNameLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("ClanNameLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("ClanNameSize")
                        .HasColumnType("int");

                    b.Property<Guid?>("FontId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAverageDuration")
                        .HasColumnType("bit");

                    b.Property<bool>("IsClanName")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLastAttackToWin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLogo")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPlayerDetails")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTotalPercentage")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTotalStars")
                        .HasColumnType("bit");

                    b.Property<int?>("LastAttackToWinLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("LastAttackToWinLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("LastAttackToWinSize")
                        .HasColumnType("int");

                    b.Property<int?>("LogoLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("LogoLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("LogoSize")
                        .HasColumnType("int");

                    b.Property<bool>("MirrorReflection")
                        .HasColumnType("bit");

                    b.Property<int?>("PlayerDetailsLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerDetailsLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerDetailsSize")
                        .HasColumnType("int");

                    b.Property<string>("TextColor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TotalPercentageLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("TotalPercentageLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("TotalPercentageSize")
                        .HasColumnType("int");

                    b.Property<int?>("TotalStarsLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("TotalStarsLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("TotalStarsSize")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("FontId");

                    b.ToTable("OverlaySettings");
                });

            modelBuilder.Entity("WarStreamer.Models.TeamLogo", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TeamName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                    b.Property<string>("ClanTags")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "TeamName");

                    b.ToTable("TeamLogos");
                });

            modelBuilder.Entity("WarStreamer.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("NewsLetter")
                        .HasColumnType("bit");

                    b.Property<long>("TierLevel")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WarStreamer.Models.WarOverlay", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("ClanTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnded")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset>("LastCheckout")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("UserId", "Id");

                    b.ToTable("WarOverlays");
                });

            modelBuilder.Entity("WarStreamer.Models.Account", b =>
                {
                    b.HasOne("WarStreamer.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarStreamer.Models.AuthToken", b =>
                {
                    b.HasOne("WarStreamer.Models.User", "User")
                        .WithOne("AuthToken")
                        .HasForeignKey("WarStreamer.Models.AuthToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarStreamer.Models.Image", b =>
                {
                    b.HasOne("WarStreamer.Models.User", "User")
                        .WithMany("Images")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarStreamer.Models.OverlaySetting", b =>
                {
                    b.HasOne("WarStreamer.Models.Font", "Font")
                        .WithMany("OverlaySettings")
                        .HasForeignKey("FontId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("WarStreamer.Models.User", "User")
                        .WithOne("OverlaySetting")
                        .HasForeignKey("WarStreamer.Models.OverlaySetting", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Font");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarStreamer.Models.TeamLogo", b =>
                {
                    b.HasOne("WarStreamer.Models.User", "User")
                        .WithMany("TeamLogos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarStreamer.Models.User", b =>
                {
                    b.HasOne("WarStreamer.Models.Language", "Language")
                        .WithMany("Users")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("WarStreamer.Models.WarOverlay", b =>
                {
                    b.HasOne("WarStreamer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WarStreamer.Models.Font", b =>
                {
                    b.Navigation("OverlaySettings");
                });

            modelBuilder.Entity("WarStreamer.Models.Language", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WarStreamer.Models.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("AuthToken")
                        .IsRequired();

                    b.Navigation("Images");

                    b.Navigation("OverlaySetting");

                    b.Navigation("TeamLogos");
                });
#pragma warning restore 612, 618
        }
    }
}