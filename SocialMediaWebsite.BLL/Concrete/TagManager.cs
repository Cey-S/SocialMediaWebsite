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
	public class TagManager : Manager<AppDbContext, Tag>, ITagManager
	{
		private readonly DbSet<Tag> _tags;

		public TagManager(AppDbContext context) : base(context)
		{
			_tags = context.Set<Tag>();
		}

		public Dictionary<string, int>? GetPopularTagCountsAsync()
		{
			return context.Tags
				.Include(t => t.Posts)  // Include related Posts
				.AsEnumerable()  // Switch to in-memory operations
				.GroupBy(t => t.TagName)
				.Select(g => new
				{
					TagName = g.Key,
					PostCount = g.Sum(t => t.Posts.Count)
				})
				.OrderByDescending(x => x.PostCount)  // Order by PostCount in descending order
				.Take(5)  // Take the top 5 tags
				.ToDictionary(x => x.TagName, x => x.PostCount);  // Convert to dictionary
		}
	}
}
