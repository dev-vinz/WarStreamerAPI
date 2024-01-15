﻿using WarStreamer.Commons.Extensions;
using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class UserMap(IUserService service) : IUserMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IUserService _service = service;

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

        public UserViewModel? GetById(string id)
        {
            Guid guid = Guid.Empty.ParseDiscordId(id);
            User? user = _service.GetById(guid);
            return user != null ? DomainToViewModel(user) : null;
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
            return new($"{domain.Id}")
            {
                LanguageId = domain.LanguageId.ToString(),
                TierLevel = domain.TierLevel,
                NewsLetter = domain.NewsLetter,
            };
        }

        private static List<UserViewModel> DomainToViewModel(List<User> domain)
        {
            return domain.Select(DomainToViewModel).ToList();
        }

        private static User ViewModelToDomain(UserViewModel viewModel)
        {
            return new(Guid.Empty.ParseDiscordId(viewModel.Id))
            {
                LanguageId = Guid.Parse(viewModel.LanguageId),
                TierLevel = viewModel.TierLevel,
                NewsLetter = viewModel.NewsLetter,
            };
        }
    }
}
