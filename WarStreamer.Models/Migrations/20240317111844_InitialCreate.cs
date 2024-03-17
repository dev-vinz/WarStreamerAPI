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
                    FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "AuthTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessIV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscordToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscordIV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpiresAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AuthTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    LocationX = table.Column<int>(type: "int", nullable: false),
                    LocationY = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => new { x.UserId, x.Name });
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
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
                    IsHeroesEquipments = table.Column<bool>(type: "bit", nullable: false),
                    HeroesEquipmentsSize = table.Column<int>(type: "int", nullable: true),
                    HeroesEquipmentLocationX = table.Column<int>(type: "int", nullable: true),
                    HeroesEquipmentLocationY = table.Column<int>(type: "int", nullable: true),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    ClanTags = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLogos", x => new { x.UserId, x.TeamName });
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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

            migrationBuilder.InsertData(
                table: "Fonts",
                columns: new[] { "Id", "DisplayName", "FamilyName", "FileName" },
                values: new object[,]
                {
                    { new Guid("07674fff-ac7f-4504-bf52-0de0702c578a"), "Clash of Clans", "Supercell-Magic", "supercell-magic.ttf" },
                    { new Guid("3bee4154-a692-48aa-8e62-94b2b349069b"), "Quicksand", "Quicksand", "quicksand.otf" },
                    { new Guid("824a4925-7e82-45a0-ae0c-2e5f4b776fdc"), "Poppins", "Poppins", "poppins.otf" },
                    { new Guid("b7c0af0a-d8ea-4fda-a872-3b7116890594"), "Roboto", "Roboto", "roboto.ttf" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "CultureInfo", "DisplayValue", "FlagEmoji", "ShortcutValue" },
                values: new object[,]
                {
                    { new Guid("0f50c7de-d1a0-4fd0-b962-0498ba33bae8"), "fr-FR", "Français", "🇫🇷", "FR" },
                    { new Guid("8e0bee00-fff2-40fb-bd99-3ef401f631fc"), "en-US", "English", "🇬🇧", "EN" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "LanguageId", "NewsLetter", "TierLevel" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("8e0bee00-fff2-40fb-bd99-3ef401f631fc"), false, 0L },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("8e0bee00-fff2-40fb-bd99-3ef401f631fc"), false, 0L },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("8e0bee00-fff2-40fb-bd99-3ef401f631fc"), false, 0L }
                });

            migrationBuilder.InsertData(
                table: "OverlaySettings",
                columns: new[] { "Id", "AverageDurationLocationX", "AverageDurationLocationY", "AverageDurationSize", "ClanNameLocationX", "ClanNameLocationY", "ClanNameSize", "FontId", "HeroesEquipmentLocationX", "HeroesEquipmentLocationY", "HeroesEquipmentsSize", "IsAverageDuration", "IsClanName", "IsHeroesEquipments", "IsLastAttackToWin", "IsLogo", "IsPlayerDetails", "IsTotalPercentage", "IsTotalStars", "LastAttackToWinLocationX", "LastAttackToWinLocationY", "LastAttackToWinSize", "LogoLocationX", "LogoLocationY", "LogoSize", "MirrorReflection", "PlayerDetailsLocationX", "PlayerDetailsLocationY", "PlayerDetailsSize", "TextColor", "TotalPercentageLocationX", "TotalPercentageLocationY", "TotalPercentageSize", "TotalStarsLocationX", "TotalStarsLocationY", "TotalStarsSize" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000000"), 365, 330, 20, 320, 220, 20, new Guid("824a4925-7e82-45a0-ae0c-2e5f4b776fdc"), 320, 495, 120, true, true, true, true, true, true, true, true, 320, 665, 14, 320, 100, 100, true, 320, 495, 120, "#FCFBF4", 365, 280, 20, 270, 305, 50 },
                    { new Guid("00000000-0000-0000-0000-000000000001"), 365, 150, 20, 320, 215, 20, new Guid("07674fff-ac7f-4504-bf52-0de0702c578a"), null, null, null, true, true, false, true, false, false, true, true, 320, 285, 10, null, null, null, false, null, null, null, "#FCFBF4", 270, 150, 20, 320, 60, 60 },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 560, 370, 20, 390, 370, 20, new Guid("3bee4154-a692-48aa-8e62-94b2b349069b"), null, null, null, true, true, false, false, true, true, true, true, null, null, null, 390, 220, 120, true, 460, 505, 100, "#FCFBF4", 550, 295, 30, 560, 190, 60 }
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
                name: "AuthTokens");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OverlaySettings");

            migrationBuilder.DropTable(
                name: "TeamLogos");

            migrationBuilder.DropTable(
                name: "WarOverlays");

            migrationBuilder.DropTable(
                name: "Fonts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");
        }
    }
}
