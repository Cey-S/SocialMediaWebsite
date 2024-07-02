using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Core.DataAccess;
using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Core.BusinessLogic
{
    public interface IManager<TContext, TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext
    {
    }
}
