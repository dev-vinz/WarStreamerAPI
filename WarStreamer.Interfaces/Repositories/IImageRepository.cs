﻿using WarStreamer.Models;

namespace WarStreamer.Interfaces.Repositories
{
    public interface IImageRepository
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public bool Delete(Image domain);

        public List<Image> GetAll();

        public Image? GetById(int id);

        public List<Image> GetByOverlaySettingId(decimal overlaySettingId);

        public Image Save(Image domain);

        public bool Update(Image domain);
    }
}