using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Models.Context
{
    public class WarStreamerContext(DbContextOptions options)
        : DbContext(options),
            IWarStreamerContext
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private IDbContextTransaction? _transaction;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public DbSet<Account> Accounts { get; set; }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Font> Fonts { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<OverlaySetting> OverlaySettings { get; set; }

        public DbSet<TeamLogo> TeamLogos { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<WarOverlay> WarOverlays { get; set; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
        }

        public void DisposeTransaction()
        {
            _transaction?.Dispose();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public new DbSet<TEntity> Set<TEntity>()
            where TEntity : Entity
        {
            return base.Set<TEntity>();
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                         PROTECTED METHODS                         *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*             OVERRIDE            *|
        \* * * * * * * * * * * * * * * * * */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* * * * * * * * * * * * * * * * * *\
            |*          CASE SETTINGS          *|
            \* * * * * * * * * * * * * * * * * */

            // Use case sensitive for all database
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CS_AS");

            // Use case-insensitive for Image name
            modelBuilder
                .Entity<Image>()
                .Property(i => i.Name)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            // Use case-insensitive for TeamLogo name
            modelBuilder
                .Entity<TeamLogo>()
                .Property(l => l.TeamName)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");

            /* * * * * * * * * * * * * * * * * *\
            |*            PROPERTIES           *|
            \* * * * * * * * * * * * * * * * * */

            // Add onDelete: SET NULL on Font
            modelBuilder
                .Entity<Font>()
                .HasMany(f => f.OverlaySettings)
                .WithOne(os => os.Font)
                .HasForeignKey(os => os.FontId)
                .OnDelete(DeleteBehavior.SetNull);

            /* * * * * * * * * * * * * * * * * *\
            |*             SEEDERS             *|
            \* * * * * * * * * * * * * * * * * */

            // Create Fond seeder
            Font cocFont = new("Clash of Clans", "Supercell-Magic", "supercell-magic.ttf");
            Font poppinsFont = new("Poppins", "Poppins", "poppins.otf");
            Font quicksandFont = new("Quicksand", "Quicksand", "quicksand.otf");
            Font robotoFont = new("Roboto", "Roboto", "roboto.ttf");

            modelBuilder.Entity<Font>().HasData(cocFont, poppinsFont, quicksandFont, robotoFont);

            // Create Language seeder
            Language defaultLang = new("en-US", "English", "EN", "🇬🇧");

            modelBuilder
                .Entity<Language>()
                .HasData(defaultLang, new Language("fr-FR", "Français", "FR", "🇫🇷"));

            // Create User seeder, for default overlay settings
            modelBuilder
                .Entity<User>()
                .HasData(
                    new User(new Guid("00000000-0000-0000-0000-000000000000"))
                    {
                        LanguageId = defaultLang.Id,
                        TierLevel = 0,
                        NewsLetter = false,
                    },
                    new User(new Guid("00000000-0000-0000-0000-000000000001"))
                    {
                        LanguageId = defaultLang.Id,
                        TierLevel = 0,
                        NewsLetter = false,
                    },
                    new User(new Guid("00000000-0000-0000-0000-000000000002"))
                    {
                        LanguageId = defaultLang.Id,
                        TierLevel = 0,
                        NewsLetter = false,
                    }
                );

            // Create OverlaySettings
            modelBuilder
                .Entity<OverlaySetting>()
                .HasData(
                    new OverlaySetting(new Guid("00000000-0000-0000-0000-000000000000"))
                    {
                        FontId = poppinsFont.Id,
                        TextColor = "#FCFBF4",
                        IsLogo = true,
                        LogoSize = 100,
                        LogoLocationX = 320,
                        LogoLocationY = 100,
                        IsClanName = true,
                        ClanNameSize = 20,
                        ClanNameLocationX = 320,
                        ClanNameLocationY = 220,
                        IsTotalStars = true,
                        TotalStarsSize = 50,
                        TotalStarsLocationX = 270,
                        TotalStarsLocationY = 305,
                        IsTotalPercentage = true,
                        TotalPercentageSize = 20,
                        TotalPercentageLocationX = 365,
                        TotalPercentageLocationY = 280,
                        IsAverageDuration = true,
                        AverageDurationSize = 20,
                        AverageDurationLocationX = 365,
                        AverageDurationLocationY = 330,
                        IsPlayerDetails = true,
                        PlayerDetailsSize = 120,
                        PlayerDetailsLocationX = 320,
                        PlayerDetailsLocationY = 495,
                        IsLastAttackToWin = true,
                        LastAttackToWinSize = 14,
                        LastAttackToWinLocationX = 320,
                        LastAttackToWinLocationY = 665,
                        IsHeroesEquipments = true,
                        HeroesEquipmentsSize = 120,
                        HeroesEquipmentLocationX = 320,
                        HeroesEquipmentLocationY = 495,
                        MirrorReflection = true,
                    },
                    new OverlaySetting(new Guid("00000000-0000-0000-0000-000000000001"))
                    {
                        FontId = cocFont.Id,
                        TextColor = "#FCFBF4",
                        IsLogo = false,
                        IsClanName = true,
                        ClanNameSize = 20,
                        ClanNameLocationX = 320,
                        ClanNameLocationY = 215,
                        IsTotalStars = true,
                        TotalStarsSize = 60,
                        TotalStarsLocationX = 320,
                        TotalStarsLocationY = 60,
                        IsTotalPercentage = true,
                        TotalPercentageSize = 20,
                        TotalPercentageLocationX = 270,
                        TotalPercentageLocationY = 150,
                        IsAverageDuration = true,
                        AverageDurationSize = 20,
                        AverageDurationLocationX = 365,
                        AverageDurationLocationY = 150,
                        IsPlayerDetails = false,
                        IsLastAttackToWin = true,
                        LastAttackToWinSize = 10,
                        LastAttackToWinLocationX = 320,
                        LastAttackToWinLocationY = 285,
                        IsHeroesEquipments = false,
                        MirrorReflection = false
                    },
                    new OverlaySetting(new Guid("00000000-0000-0000-0000-000000000002"))
                    {
                        FontId = quicksandFont.Id,
                        TextColor = "#FCFBF4",
                        IsLogo = true,
                        LogoSize = 120,
                        LogoLocationX = 390,
                        LogoLocationY = 220,
                        IsClanName = true,
                        ClanNameSize = 20,
                        ClanNameLocationX = 390,
                        ClanNameLocationY = 370,
                        IsTotalStars = true,
                        TotalStarsSize = 60,
                        TotalStarsLocationX = 560,
                        TotalStarsLocationY = 190,
                        IsTotalPercentage = true,
                        TotalPercentageSize = 30,
                        TotalPercentageLocationX = 550,
                        TotalPercentageLocationY = 295,
                        IsAverageDuration = true,
                        AverageDurationSize = 20,
                        AverageDurationLocationX = 560,
                        AverageDurationLocationY = 370,
                        IsPlayerDetails = true,
                        PlayerDetailsSize = 100,
                        PlayerDetailsLocationX = 460,
                        PlayerDetailsLocationY = 505,
                        IsLastAttackToWin = false,
                        IsHeroesEquipments = false,
                        MirrorReflection = true,
                    }
                );
        }
    }
}
