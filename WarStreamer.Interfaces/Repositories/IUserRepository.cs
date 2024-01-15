using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(User domain);

        public List<User> GetAll();

        public User? GetById(Guid id);

        public User Save(User domain);

        public bool Update(User domain);
    }
}
