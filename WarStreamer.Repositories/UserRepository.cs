using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserRepository(IWarStreamerContext context) : base(context) { }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(User domain)
        {
            try
            {
                Remove(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<User> GetAll()
        {
            try
            {
                return Context.Set<User>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User? GetById(decimal id)
        {
            try
            {
                return Context.Set<User>().FirstOrDefault(u => u.Id == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Save(User domain)
        {
            try
            {
                domain.CreatedAt = DateTimeOffset.UtcNow;
                domain.UpdatedAt = domain.CreatedAt;

                return Insert(domain);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(User domain)
        {
            try
            {
                domain.UpdatedAt = DateTimeOffset.UtcNow;
                Update<User>(domain);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
