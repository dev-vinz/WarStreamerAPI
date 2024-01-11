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

            // Add onDelete: SET NULL on Font
            modelBuilder
                .Entity<Font>()
                .HasMany(f => f.OverlaySettings)
                .WithOne(os => os.Font)
                .HasForeignKey(os => os.FontId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
