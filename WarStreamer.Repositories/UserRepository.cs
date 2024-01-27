using WarStreamer.Interfaces.Repositories;
using WarStreamer.Models;
using WarStreamer.Models.Context;
using WarStreamer.Repositories.RepositoryBase;

namespace WarStreamer.Repositories
{
    public class UserRepository(IWarStreamerContext context) : Repository(context), IUserRepository
    {
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

        public User? GetById(Guid id)
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
                Insert(domain);

                return domain;
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
