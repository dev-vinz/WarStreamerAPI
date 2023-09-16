﻿using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class AccountMap : IAccountMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IAccountService _service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AccountMap(IAccountService service)
        {
            _service = service;
        }

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

            if (account == null) return null;

            return DomainToViewModel(account);
        }

        public List<AccountViewModel> GetByUserId(string userId)
        {
            if (!decimal.TryParse(userId, out decimal decimalUserId)) throw new FormatException($"Cannot parse '{userId}' to decimal");

            return DomainToViewModel(_service.GetByUserId(decimalUserId));
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static AccountViewModel DomainToViewModel(Account domain)
        {
            return new(domain.Tag, domain.UserId.ToString());
        }

        private static List<AccountViewModel> DomainToViewModel(List<Account> domain)
        {
            return domain
                .Select(DomainToViewModel)
                .ToList();
        }

        private static Account ViewModelToDomain(AccountViewModel viewModel)
        {
            return new(viewModel.Tag, decimal.Parse(viewModel.UserId));
        }
    }
}
