﻿using Microsoft.EntityFrameworkCore;
using Entity = WarStreamer.Models.EntityBase.EntityBase;

namespace WarStreamer.Models.Context
{
    public interface IWarStreamerContext
    {
        /* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *\
        |*                           PUBLIC METHODS                          *|
        \* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public void BeginTransaction();

        public void CommitTransaction();

        public void DisposeTransaction();

        public void RollbackTransaction();

        public void SaveChanges();

        public DbSet<TEntity> Set<TEntity>() where TEntity : Entity;
    }
}
