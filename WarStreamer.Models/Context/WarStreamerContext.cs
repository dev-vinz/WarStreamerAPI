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

        public DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }

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
            modelBuilder
                .Entity<Font>()
                .HasData(
                    new Font("Clash of Clans", "supercell-magic.ttf"),
                    new Font("Poppins", "poppins.otf"),
                    new Font("Quicksand", "quicksand.otf"),
                    new Font("Roboto", "roboto.ttf")
                );

            // Create Language seeder
            modelBuilder
                .Entity<Language>()
                .HasData(
                    new Language("en-US", "English", "en", "🇬🇧"),
                    new Language("fr-FR", "Français", "fr", "🇫🇷")
                );
        }
    }
}
