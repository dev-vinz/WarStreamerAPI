﻿using WarStreamer.Interfaces.Repositories;
using WarStreamer.Interfaces.Services;
using WarStreamer.Models;

namespace WarStreamer.Services
{
    public class TeamLogoService(ITeamLogoRepository repository) : ITeamLogoService
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                               FIELDS                              *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        private readonly ITeamLogoRepository _repository = repository;

        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public TeamLogo Create(TeamLogo domain)
        {
            return _repository.Save(domain);
        }

        public bool Delete(TeamLogo domain)
        {
            return _repository.Delete(domain);
        }

        public List<TeamLogo> GetAll()
        {
            return _repository.GetAll();
        }

        public List<TeamLogo> GetByUserId(string userId)
        {
            return _repository.GetByUserId(userId);
        }

        public TeamLogo? GetByUserIdAndName(string userId, string name)
        {
            return _repository.GetByUserIdAndName(userId, name);
        }

        public bool Update(TeamLogo domain)
        {
            return _repository.Update(domain);
        }
    }
}
