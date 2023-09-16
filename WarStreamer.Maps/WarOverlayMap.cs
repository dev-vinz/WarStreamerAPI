﻿using WarStreamer.Interfaces.Maps;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;
using WarStreamer.ViewModels;

namespace WarStreamer.Maps
{
    public class WarOverlayMap : IWarOverlayMap
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly IWarOverlayService _service;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                            CONSTRUCTORS                           *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayMap(IWarOverlayService service)
        {
            _service = service;
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public WarOverlayViewModel Create(WarOverlayViewModel viewModel)
        {
            WarOverlay overlay = ViewModelToDomain(viewModel);
            return DomainToViewModel(_service.Create(overlay));
        }

        public bool Delete(WarOverlayViewModel viewModel)
        {
            WarOverlay overlay = ViewModelToDomain(viewModel);
            return _service.Delete(overlay);
        }

        public List<WarOverlayViewModel> GetAll()
        {
            return DomainToViewModel(_service.GetAll());
        }

        public List<WarOverlayViewModel> GetByUserId(string userId)
        {
            if (!decimal.TryParse(userId, out decimal decimalUserId)) throw new FormatException($"Cannot parse '{userId}' to decimal");

            return DomainToViewModel(_service.GetByUserId(decimalUserId));
        }

        public WarOverlayViewModel? GetByUserIdAndId(string userId, int id)
        {
            if (!decimal.TryParse(userId, out decimal decimalUserId)) throw new FormatException($"Cannot parse '{userId}' to decimal");

            WarOverlay? overlay = _service.GetByUserIdAndId(decimalUserId, id);

            if (overlay == null) return null;

            return DomainToViewModel(overlay);
        }

        public bool Update(WarOverlayViewModel viewModel)
        {
            WarOverlay overlay = ViewModelToDomain(viewModel);
            return _service.Update(overlay);
        }

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                          PRIVATE METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        /* * * * * * * * * * * * * * * * * *\
        |*              STATIC             *|
        \* * * * * * * * * * * * * * * * * */

        private static WarOverlayViewModel DomainToViewModel(WarOverlay domain)
        {
            return new(domain.UserId.ToString(), domain.Id, domain.ClanTag)
            {
                LastCheckout = domain.LastCheckout,
                IsEnded = domain.IsEnded,
            };
        }

        private static List<WarOverlayViewModel> DomainToViewModel(List<WarOverlay> domain)
        {
            return domain
                .Select(DomainToViewModel)
                .ToList();
        }

        private static WarOverlay ViewModelToDomain(WarOverlayViewModel viewModel)
        {
            return new(decimal.Parse(viewModel.UserId), viewModel.Id, viewModel.ClanTag)
            {
                LastCheckout = viewModel.LastCheckout,
                IsEnded = viewModel.IsEnded,
            };
        }
    }
}
