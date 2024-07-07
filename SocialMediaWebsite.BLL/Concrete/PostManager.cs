using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.BLL.Concrete
{
	public class PostManager : Manager<AppDbContext, Post>, IPostManager
	{
		public PostManager(AppDbContext context) : base(context)
		{
		}

		public async Task<List<Post>?> SkipAndTakePosts(int pageIndex, int pageSize)
		{
			return await context.Set<Post>().Skip(pageIndex * pageSize).Take(pageSize).Include(p => p.Owner).Include(p => p.Tags).ToListAsync();
		}
	}
}
