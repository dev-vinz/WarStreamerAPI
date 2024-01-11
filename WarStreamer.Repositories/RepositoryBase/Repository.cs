using Microsoft.EntityFrameworkCore.ChangeTracking;
using WarStreamer.Models.Context;
using WarStreamer.Models.EntityBase;

namespace WarStreamer.Repositories.RepositoryBase
{
    public class Repository(IWarStreamerContext context)
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWarStreamerContext _context = context;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                             PROPERTIES                            *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public IWarStreamerContext Context
        {
            get => _context;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public void Remove<TEntity>(TEntity domain)
            where TEntity : Entity
        {
            _context.BeginTransaction();

            try
            {
                Entity? existing = _context.Set<TEntity>().FirstOrDefault(t => t == domain);

                if (existing != null)
                {
                    _context.Set<TEntity>().Remove((TEntity)existing);
                }

                _context.SaveChanges();
                _context.CommitTransaction();
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();

                Console.Error.WriteLine(ex.ToString());
            }
            finally
            {
                _context.DisposeTransaction();
            }
        }

        public TEntity? Insert<TEntity>(TEntity domain)
            where TEntity : Entity
        {
            _context.BeginTransaction();

            try
            {
                EntityEntry<TEntity> addedDomain = _context.Set<TEntity>().Add(domain);

                _context.SaveChanges();
                _context.CommitTransaction();

                return addedDomain.Entity;
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();

                Console.Error.WriteLine(ex.ToString());

                return null;
            }
            finally
            {
                _context.DisposeTransaction();
            }
        }

        public void Update<TEntity>(TEntity domain)
            where TEntity : Entity
        {
            _context.BeginTransaction();

            try
            {
                Entity? existing = Context.Set<TEntity>().FirstOrDefault(t => t == domain);

                if (existing != null)
                {
                    domain.CopyTo(ref existing);
                }

                _context.SaveChanges();
                _context.CommitTransaction();
            }
            catch (Exception ex)
            {
                _context.RollbackTransaction();

                Console.Error.WriteLine(ex.ToString());
            }
            finally
            {
                _context.DisposeTransaction();
            }
        }
    }
}
