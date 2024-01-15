using WarStreamer.Models;

namespace WarStreamer.Interfaces.Services
{
    public interface IUserService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public User Create(User domain);

        public bool Delete(User domain);

        public List<User> GetAll();

        public User? GetById(Guid id);

        public bool Update(User domain);
    }
}
