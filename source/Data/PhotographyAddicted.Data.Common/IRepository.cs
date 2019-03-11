﻿using System;
using System.Linq;
using System.Threading.Tasks;

namespace PhotographyAddicted.Data.Common
{
    public interface IRepository <TEntity> where TEntity: class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();
        
    }
}
