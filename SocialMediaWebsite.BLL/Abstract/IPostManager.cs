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
	public interface IPostManager : IManager<AppDbContext, Post>
	{
		Task<List<Post>?> SkipAndTakePosts(int pageIndex, int pageSize, int firstPostId);
		Task<List<Post>?> SkipAndTakePopularPosts(int pageIndex, int pageSize);
		Task<List<Post>?> SkipAndTakeFollowingPosts(int pageIndex, int pageSize, int firstPostId, string username);
		Task<List<Post>?> SkipAndTakeProfilePosts(int pageIndex, int pageSize, int firstPostId, string username);
		Task<List<Post>?> SkipAndTakeInteractedPosts(int pageIndex, int pageSize, int interactionType, string userId);
		Task<List<Post>?> SkipAndTakePostsWithTag(int pageIndex, int pageSize, int firstPostId, string tag);
	}
}
