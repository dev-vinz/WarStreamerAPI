using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class OverlaySettingService(IOverlaySettingRepository repository)
        : IOverlaySettingService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IOverlaySettingRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public OverlaySetting Create(OverlaySetting domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(OverlaySetting domain)
        {
            return _repository.Delete(domain);
        }

        public List<OverlaySetting> GetAll()
        {
            return _repository.GetAll();
        }

        public OverlaySetting? GetByUserId(Guid userId)
        {
            return _repository.GetByUserId(userId);
        }

        public bool Update(OverlaySetting domain)
        {
            return _repository.Update(domain);
        }
    }
}
