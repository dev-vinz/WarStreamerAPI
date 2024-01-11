﻿using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class ImageService(IImageRepository repository) : IImageService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IImageRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public Image Create(Image domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(Image domain)
        {
            return _repository.Delete(domain);
        }

        public List<Image> GetAll()
        {
            return _repository.GetAll();
        }

        public Image? GetByOverlaySettingIdAndName(decimal overlaySettingId, string name)
        {
            return _repository.GetByOverlaySettingIdAndName(overlaySettingId, name);
        }

        public List<Image> GetByOverlaySettingId(decimal overlaySettingId)
        {
            return _repository.GetByOverlaySettingId(overlaySettingId);
        }

        public bool Update(Image domain)
        {
            return _repository.Update(domain);
        }
    }
}
