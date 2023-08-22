using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class UserService : IUserService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IUserRepository _repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public User Create(User domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(User domain)
        {
            return _repository.Delete(domain);
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }

        public User? GetById(decimal id)
        {
            return _repository.GetById(id);
        }

        public bool Update(User domain)
        {
            return _repository.Update(domain);
        }
    }
}
