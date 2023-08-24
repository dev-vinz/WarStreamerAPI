using WarStreamer.ViewModels;

namespace WarStreamer.Interfaces.Maps
{
    public interface IUserMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public UserViewModel Create(UserViewModel viewModel);

        public bool Delete(UserViewModel viewModel);

        public List<UserViewModel> GetAll();

        public UserViewModel? GetById(decimal id);

        public bool Update(UserViewModel viewModel);
    }
}
