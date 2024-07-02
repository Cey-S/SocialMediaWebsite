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
    public class Manager<TContext, TEntity> : Repository<TContext, TEntity>, IManager<TContext, TEntity>
       where TEntity : BaseEntity
       where TContext : DbContext
    {
        public readonly TContext context;

        public Manager(TContext context) : base(context)
        {
            this.context = context;
        }
    }
}
