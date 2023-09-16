using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class UserMap : IUserMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IUserService _service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserMap(IUserService service)
        {
            _service = service;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserViewModel Create(UserViewModel viewModel)
        {
            User user = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(user));
        }

        public bool Delete(UserViewModel viewModel)
        {
            User user = ViewModelToDomain(viewModel);
            return _service.Delete(user);
        }

        public List<UserViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public UserViewModel? GetById(decimal id)
        {
            User? user = _service.GetById(id);

            if (user == null) return null;

            return DomainToViewModel(user);
        }

        public bool Update(UserViewModel viewModel)
        {
            User user = ViewModelToDomain(viewModel);
            return _service.Update(user);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static UserViewModel DomainToViewModel(User domain)
        {
            return new(domain.Id.ToString())
            {
                LanguageId = domain.LanguageId,
                TierLevel = domain.TierLevel,
            };
        }

        private static List<UserViewModel> DomainToViewModel(List<User> domain)
        {
            return domain
                .Select(DomainToViewModel)
                .ToList();
        }

        private static User ViewModelToDomain(UserViewModel viewModel)
        {
            return new(decimal.Parse(viewModel.Id))
            {
                LanguageId = viewModel.LanguageId,
                TierLevel = viewModel.TierLevel,
            };
        }
    }
}
