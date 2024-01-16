using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WarStreamer.Models.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fonts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CultureInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortcutValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlagEmoji = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TierLevel = table.Column<long>(type: "bigint", nullable: false),
                    NewsLetter = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Tag);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OverlaySettings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FontId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TextColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsLogo = table.Column<bool>(type: "bit", nullable: false),
                    LogoSize = table.Column<int>(type: "int", nullable: true),
                    LogoLocationX = table.Column<int>(type: "int", nullable: true),
                    LogoLocationY = table.Column<int>(type: "int", nullable: true),
                    IsClanName = table.Column<bool>(type: "bit", nullable: false),
                    ClanNameSize = table.Column<int>(type: "int", nullable: true),
                    ClanNameLocationX = table.Column<int>(type: "int", nullable: true),
                    ClanNameLocationY = table.Column<int>(type: "int", nullable: true),
                    IsTotalStars = table.Column<bool>(type: "bit", nullable: false),
                    TotalStarsSize = table.Column<int>(type: "int", nullable: true),
                    TotalStarsLocationX = table.Column<int>(type: "int", nullable: true),
                    TotalStarsLocationY = table.Column<int>(type: "int", nullable: true),
                    IsTotalPercentage = table.Column<bool>(type: "bit", nullable: false),
                    TotalPercentageSize = table.Column<int>(type: "int", nullable: true),
                    TotalPercentageLocationX = table.Column<int>(type: "int", nullable: true),
                    TotalPercentageLocationY = table.Column<int>(type: "int", nullable: true),
                    IsAverageDuration = table.Column<bool>(type: "bit", nullable: false),
                    AverageDurationSize = table.Column<int>(type: "int", nullable: true),
                    AverageDurationLocationX = table.Column<int>(type: "int", nullable: true),
                    AverageDurationLocationY = table.Column<int>(type: "int", nullable: true),
                    IsPlayerDetails = table.Column<bool>(type: "bit", nullable: false),
                    PlayerDetailsSize = table.Column<int>(type: "int", nullable: true),
                    PlayerDetailsLocationX = table.Column<int>(type: "int", nullable: true),
                    PlayerDetailsLocationY = table.Column<int>(type: "int", nullable: true),
                    IsLastAttackToWin = table.Column<bool>(type: "bit", nullable: false),
                    LastAttackToWinSize = table.Column<int>(type: "int", nullable: true),
                    LastAttackToWinLocationX = table.Column<int>(type: "int", nullable: true),
                    LastAttackToWinLocationY = table.Column<int>(type: "int", nullable: true),
                    MirrorReflection = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverlaySettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverlaySettings_Fonts_FontId",
                        column: x => x.FontId,
                        principalTable: "Fonts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_OverlaySettings_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamLogos",
                columns: table => new
                {
                    TeamName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClanTags = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLogos", x => new { x.TeamName, x.UserId });
                    table.ForeignKey(
                        name: "FK_TeamLogos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarOverlays",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClanTag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastCheckout = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsEnded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarOverlays", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_WarOverlays_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    OverlaySettingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LocationX = table.Column<int>(type: "int", nullable: false),
                    LocationY = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => new { x.OverlaySettingId, x.Name });
                    table.ForeignKey(
                        name: "FK_Images_OverlaySettings_OverlaySettingId",
                        column: x => x.OverlaySettingId,
                        principalTable: "OverlaySettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fonts",
                columns: new[] { "Id", "DisplayName", "FileName" },
                values: new object[,]
                {
                    { new Guid("07972212-5ddb-47dd-a0cd-a9e4fc64ddb6"), "Clash of Clans", "supercell-magic.ttf" },
                    { new Guid("a7a6ba48-34b6-40bf-a0c4-49f3cd3bd41a"), "Poppins", "poppins.otf" },
                    { new Guid("b11116d7-25cc-4f50-aac3-1836e0a2ef02"), "Roboto", "roboto.ttf" },
                    { new Guid("b588379e-318f-4c44-bcbe-4bd98d7a89b4"), "Quicksand", "quicksand.otf" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CultureInfo", "DisplayValue", "FlagEmoji", "ShortcutValue" },
                values: new object[,]
                {
                    { new Guid("27dd4bf1-bfe2-4fa3-ba93-5e4b1a602fe3"), "fr-FR", "Français", "🇫🇷", "fr" },
                    { new Guid("31f146ba-6dd9-4d33-ab6f-7ce8b5d93205"), "en-US", "English", "🇬🇧", "en" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OverlaySettings_FontId",
                table: "OverlaySettings",
                column: "FontId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLogos_UserId",
                table: "TeamLogos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "TeamLogos");

            migrationBuilder.DropTable(
                name: "WarOverlays");

            migrationBuilder.DropTable(
                name: "OverlaySettings");

            migrationBuilder.DropTable(
                name: "Fonts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
