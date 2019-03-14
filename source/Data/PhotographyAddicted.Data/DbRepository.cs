using Microsoft.EntityFrameworkCore;
using PhotographyAddicted.Data.Common;
using PhotographyAddicted.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotographyAddicted.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable where TEntity : class
    {

        private PhotographyAddictedContext context;

        private  DbSet<TEntity> DbSet;

        public DbRepository(PhotographyAddictedContext context)
        {
            this.context = context;

            this.DbSet = this.context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            return this.DbSet.AddAsync(entity);
        }

        public System.Linq.IQueryable<TEntity> All()
        {
            return this.DbSet;
        }

        public void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

    }
}
