﻿using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IUserMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserViewModel Create(UserViewModel viewModel);

        public bool Delete(UserViewModel viewModel);

        public UserViewModel? GetById(string id);

        public bool Update(UserViewModel viewModel);
    }
}
