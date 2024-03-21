﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WarStreamer.Models.Context;

#nullable disable

namespace WarStreamer.Models.Migrations
{
    [DbContext(typeof(WarStreamerContext))]
    partial class WarStreamerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CS_AS")
                .HasAnnotation("ProductVersion", "8.0.3")
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

                    b.Property<string>("FamilyName")
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
                            Id = new Guid("e7b1e3f0-d1db-466e-9fbc-a88064d82b01"),
                            DisplayName = "Clash of Clans",
                            FamilyName = "Supercell-Magic",
                            FileName = "supercell-magic.woff"
                        },
                        new
                        {
                            Id = new Guid("76803f46-8876-4767-97d0-a378c69b4768"),
                            DisplayName = "Poppins",
                            FamilyName = "Poppins",
                            FileName = "poppins.woff"
                        },
                        new
                        {
                            Id = new Guid("9ca2d461-0c9f-4c71-bc7f-761cf4635797"),
                            DisplayName = "Quicksand",
                            FamilyName = "Quicksand",
                            FileName = "quicksand.woff"
                        },
                        new
                        {
                            Id = new Guid("210429cb-dd49-47e3-b8a5-99f4b1ac00c7"),
                            DisplayName = "Roboto",
                            FamilyName = "Roboto",
                            FileName = "roboto.woff"
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
                            Id = new Guid("56b86e3d-9109-4579-a3e3-1d07cfe942f7"),
                            CultureInfo = "en-US",
                            DisplayValue = "English",
                            FlagEmoji = "🇬🇧",
                            ShortcutValue = "EN"
                        },
                        new
                        {
                            Id = new Guid("85ecc9b5-7aba-4e81-8cab-191930e3ca74"),
                            CultureInfo = "fr-FR",
                            DisplayValue = "Français",
                            FlagEmoji = "🇫🇷",
                            ShortcutValue = "FR"
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

                    b.Property<string>("BackgroundColor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ClanNameLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("ClanNameLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("ClanNameSize")
                        .HasColumnType("int");

                    b.Property<Guid?>("FontId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<int?>("HeroesEquipmentLocationX")
                        .HasColumnType("int");

                    b.Property<int?>("HeroesEquipmentLocationY")
                        .HasColumnType("int");

                    b.Property<int?>("HeroesEquipmentsSize")
                        .HasColumnType("int");

                    b.Property<bool>("IsAverageDuration")
                        .HasColumnType("bit");

                    b.Property<bool>("IsClanName")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHeroesEquipments")
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

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("FontId");

                    b.ToTable("OverlaySettings");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000000"),
                            AverageDurationLocationX = 365,
                            AverageDurationLocationY = 330,
                            AverageDurationSize = 20,
                            BackgroundColor = "#00FF00",
                            ClanNameLocationX = 320,
                            ClanNameLocationY = 220,
                            ClanNameSize = 20,
                            FontId = new Guid("76803f46-8876-4767-97d0-a378c69b4768"),
                            Height = 720,
                            HeroesEquipmentLocationX = 320,
                            HeroesEquipmentLocationY = 495,
                            HeroesEquipmentsSize = 120,
                            IsAverageDuration = true,
                            IsClanName = true,
                            IsHeroesEquipments = true,
                            IsLastAttackToWin = true,
                            IsLogo = true,
                            IsPlayerDetails = true,
                            IsTotalPercentage = true,
                            IsTotalStars = true,
                            LastAttackToWinLocationX = 320,
                            LastAttackToWinLocationY = 665,
                            LastAttackToWinSize = 14,
                            LogoLocationX = 320,
                            LogoLocationY = 100,
                            LogoSize = 100,
                            MirrorReflection = true,
                            PlayerDetailsLocationX = 320,
                            PlayerDetailsLocationY = 495,
                            PlayerDetailsSize = 120,
                            TextColor = "#FCFBF4",
                            TotalPercentageLocationX = 365,
                            TotalPercentageLocationY = 280,
                            TotalPercentageSize = 20,
                            TotalStarsLocationX = 270,
                            TotalStarsLocationY = 305,
                            TotalStarsSize = 50,
                            Width = 1280
                        },
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000001"),
                            AverageDurationLocationX = 100,
                            AverageDurationLocationY = 150,
                            AverageDurationSize = 20,
                            BackgroundColor = "#00FF00",
                            ClanNameLocationX = 145,
                            ClanNameLocationY = 215,
                            ClanNameSize = 20,
                            FontId = new Guid("e7b1e3f0-d1db-466e-9fbc-a88064d82b01"),
                            Height = 330,
                            IsAverageDuration = true,
                            IsClanName = true,
                            IsHeroesEquipments = false,
                            IsLastAttackToWin = true,
                            IsLogo = false,
                            IsPlayerDetails = false,
                            IsTotalPercentage = true,
                            IsTotalStars = true,
                            LastAttackToWinLocationX = 145,
                            LastAttackToWinLocationY = 285,
                            LastAttackToWinSize = 10,
                            MirrorReflection = false,
                            TextColor = "#FCFBF4",
                            TotalPercentageLocationX = 195,
                            TotalPercentageLocationY = 150,
                            TotalPercentageSize = 20,
                            TotalStarsLocationX = 145,
                            TotalStarsLocationY = 60,
                            TotalStarsSize = 60,
                            Width = 580
                        },
                        new
                        {
                            UserId = new Guid("00000000-0000-0000-0000-000000000002"),
                            AverageDurationLocationX = 280,
                            AverageDurationLocationY = 260,
                            AverageDurationSize = 20,
                            BackgroundColor = "#00FF00",
                            ClanNameLocationX = 130,
                            ClanNameLocationY = 260,
                            ClanNameSize = 20,
                            FontId = new Guid("9ca2d461-0c9f-4c71-bc7f-761cf4635797"),
                            Height = 510,
                            IsAverageDuration = true,
                            IsClanName = true,
                            IsHeroesEquipments = false,
                            IsLastAttackToWin = false,
                            IsLogo = true,
                            IsPlayerDetails = true,
                            IsTotalPercentage = true,
                            IsTotalStars = true,
                            LogoLocationX = 110,
                            LogoLocationY = 110,
                            LogoSize = 120,
                            MirrorReflection = true,
                            PlayerDetailsLocationX = 180,
                            PlayerDetailsLocationY = 395,
                            PlayerDetailsSize = 100,
                            TextColor = "#FCFBF4",
                            TotalPercentageLocationX = 270,
                            TotalPercentageLocationY = 185,
                            TotalPercentageSize = 30,
                            TotalStarsLocationX = 280,
                            TotalStarsLocationY = 80,
                            TotalStarsSize = 60,
                            Width = 720
                        });
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000000"),
                            LanguageId = new Guid("56b86e3d-9109-4579-a3e3-1d07cfe942f7"),
                            NewsLetter = false,
                            TierLevel = 0L
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            LanguageId = new Guid("56b86e3d-9109-4579-a3e3-1d07cfe942f7"),
                            NewsLetter = false,
                            TierLevel = 0L
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            LanguageId = new Guid("56b86e3d-9109-4579-a3e3-1d07cfe942f7"),
                            NewsLetter = false,
                            TierLevel = 0L
                        });
                });

            modelBuilder.Entity("WarStreamer.Models.WarOverlay", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

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
