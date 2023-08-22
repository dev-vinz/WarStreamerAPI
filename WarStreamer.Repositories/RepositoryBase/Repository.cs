using Microsoft.EntityFrameworkCore.ChangeTracking;
using WarStreamer.Models.Context;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Repositories.RepositoryBase
{
    public class Repository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWarStreamerContext _context;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public IWarStreamerContext Context { get => _context; }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Repository(IWarStreamerContext context)
        {
            _context = context;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public void Remove<TEntity>(TEntity domain) where TEntity : Entity
        {
            _context.BeginTransaction();

            Entity? existing = _context.Set<TEntity>().FirstOrDefault(t => t == domain);

            if (existing != null) _context.Set<TEntity>().Remove((TEntity)existing);

            _context.SaveChanges();
            _context.CommitTransaction();

            _context.DisposeTransaction();
        }

        public TEntity Insert<TEntity>(TEntity domain) where TEntity : Entity
        {
            _context.BeginTransaction();

            EntityEntry<TEntity> addedDomain = _context.Set<TEntity>().Add(domain);

            _context.SaveChanges();
            _context.CommitTransaction();

            _context.DisposeTransaction();

            return addedDomain.Entity;
        }

        public void Update<TEntity>(TEntity domain) where TEntity : Entity
        {
            _context.BeginTransaction();

            Entity? existing = Context.Set<TEntity>().FirstOrDefault(t => t == domain);

            if (existing != null) domain.CopyTo(ref existing);

            _context.SaveChanges();
            _context.CommitTransaction();

            _context.DisposeTransaction();
        }
    }
}
