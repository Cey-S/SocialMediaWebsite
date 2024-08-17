using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.BLL.Abstract
{
	public interface ITagManager : IManager<AppDbContext, Tag>
	{
		Dictionary<string, int>? GetPopularTagCounts();
	}
}
