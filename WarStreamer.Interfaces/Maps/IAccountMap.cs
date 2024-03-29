﻿using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IAccountMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public AccountViewModel Create(AccountViewModel viewModel);

        public bool Delete(AccountViewModel viewModel);

        public AccountViewModel? GetByTag(string tag);

        public List<AccountViewModel> GetByUserId(string userId);
    }
}
