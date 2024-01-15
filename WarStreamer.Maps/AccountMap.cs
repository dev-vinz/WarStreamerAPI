using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class AccountMap(IAccountService service) : IAccountMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAccountService _service = service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AccountViewModel Create(AccountViewModel viewModel)
        {
            Account account = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(account));
        }

        public bool Delete(AccountViewModel viewModel)
        {
            Account account = ViewModelToDomain(viewModel);
            return _service.Delete(account);
        }

        public List<AccountViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public AccountViewModel? GetByTag(string tag)
        {
            Account? account = _service.GetByTag(tag);
            return account != null ? DomainToViewModel(account) : null;
        }

        public List<AccountViewModel> GetByUserId(string userId)
        {
            Guid guid = Guid.Empty.ParseDiscordId(userId);
            return DomainToViewModel(_service.GetByUserId(guid));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AccountViewModel DomainToViewModel(Account domain)
        {
            return new(domain.Tag, $"{domain.UserId}");
        }

        private static List<AccountViewModel> DomainToViewModel(List<Account> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static Account ViewModelToDomain(AccountViewModel viewModel)
        {
            return new(viewModel.Tag, Guid.Empty.ParseDiscordId(viewModel.UserId));
        }
    }
}
