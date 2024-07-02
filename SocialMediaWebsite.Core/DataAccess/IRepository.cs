using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> InsertAsync(TEntity input);
        Task<int> UpdateAsync(TEntity input);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(TEntity input);
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task<List<TEntity>?> GetAllIncludeAsync(Expression<Func<TEntity, bool>>? filter, params Expression<Func<TEntity, object>>[] include);
    }
}
