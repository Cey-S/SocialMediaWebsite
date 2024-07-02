using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Core.DataAccess
{
    public class Repository<TContext, TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
        private readonly TContext context;
        private readonly DbSet<TEntity> dbSet;

        public Repository(TContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<int> InsertAsync(TEntity input)
        {
            await dbSet.AddAsync(input);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity input)
        {
            dbSet.Update(input);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return 0;
            }
            dbSet.Remove(entity);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity input)
        {
            dbSet.Remove(input);
            return await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
        {
            if (filter != null)
                return await dbSet.Where(filter).ToListAsync();
            else
                return await dbSet.ToListAsync();
        }

        public async Task<List<TEntity>?> GetAllIncludeAsync(Expression<Func<TEntity, bool>>? filter, params Expression<Func<TEntity, object>>[] include)
        {
            IQueryable<TEntity> query;
            if (filter != null)
            {
                query = dbSet.Where(filter);
            }
            else
            {
                query = dbSet;
            }

            return await include.Aggregate(query, (current, includeProperty) => current.Include(includeProperty)).ToListAsync();

        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
    }
}
