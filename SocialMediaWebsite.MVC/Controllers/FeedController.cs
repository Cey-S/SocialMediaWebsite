using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class FeedController : ControllerBase
	{
		private readonly IPostManager postManager;
		private readonly UserManager<MyUser> userManager;

		public FeedController(IPostManager postManager, UserManager<MyUser> userManager)
		{
			this.postManager = postManager;
			this.userManager = userManager;
		}

		// Home Feed Posts: Latest, All posts order by descending
		[HttpGet]
		public async Task<ActionResult> GetLatestData(int pageIndex, int pageSize, int firstPostId)
		{
			var posts = await postManager.SkipAndTakePosts(pageIndex, pageSize, firstPostId);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var json = await ConvertPostsToJsonAsync(posts);
			return Ok(json);
		}

		// Home Feed Posts: Popular, All posts ordered by interaction counts
		[HttpGet]
		public async Task<ActionResult> GetPopularData(int pageIndex, int pageSize, int firstPostId)
		{
			var posts = await postManager.SkipAndTakePopularPosts(pageIndex, pageSize);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var json = await ConvertPostsToJsonAsync(posts);
			return Ok(json);
		}

		// Home Feed Posts: Following, Posts made by accounts the logged-in user follows
		[HttpGet]
		public async Task<ActionResult> GetFollowingData(int pageIndex, int pageSize, int firstPostId)
		{
			var posts = await postManager.SkipAndTakeFollowingPosts(pageIndex, pageSize, firstPostId, User.Identity.Name);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var json = await ConvertPostsToJsonAsync(posts);
			return Ok(json);
		}

		// User Profile Posts
		[HttpGet]
		public async Task<ActionResult> GetProfileData(int pageIndex, int pageSize, int firstPostId, string username)
		{
			var posts = await postManager.SkipAndTakeProfilePosts(pageIndex, pageSize, firstPostId, username);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var json = await ConvertPostsToJsonAsync(posts);
			return Ok(json);
		}

		// Posts with spesific tag name
		[HttpGet]
		public async Task<ActionResult> GetDataWithTag(int pageIndex, int pageSize, int firstPostId, string tag)
		{
			var posts = await postManager.SkipAndTakePostsWithTag(pageIndex, pageSize, firstPostId, tag);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var json = await ConvertPostsToJsonAsync(posts);
			return Ok(json);
		}

		// Accounts that contain the searched word in their name
		[HttpGet]
		public async Task<ActionResult> GetAccountsWithUsername(int pageIndex, int pageSize, string searchedWord)
		{
			var accountList = await userManager.Users.AsNoTracking()
									 .Where(p => p.UserName.Contains(searchedWord))
									 .OrderBy(p => p.UserName)
									 .Skip(pageIndex * pageSize)
									 .Take(pageSize)
									 .ToListAsync();

			if (accountList == null || accountList.Count == 0)
			{
				return Ok(null);
			}

			var match = accountList.Find(p => p.UserName.ToLower() == searchedWord.ToLower());
			if (match != null)
			{
				accountList.Remove(match);
				accountList.Insert(0, match); // Move the exact match to the top of the list
			}

			List<FollowersVM> accountVMs = new List<FollowersVM>();
			accountList.ForEach(a =>
			{
				accountVMs.Add(new FollowersVM { ImagePath = a.ImagePath, Username = a.UserName });
			});

			var json = JsonConvert.SerializeObject(accountVMs);
			return Ok(json);
		}

		private async Task<string> ConvertPostsToJsonAsync(List<Post> posts)
		{
			List<PostVM> postVMs = new List<PostVM>();

			var signedInUser = await userManager.GetUserAsync(User);

			posts.ForEach(p =>
			{
				List<string> postTags = new List<string>();
				p.Tags.ForEach(t => { postTags.Add(t.TagName); });

				bool isLikedByUser = p.Interactions.Where(i => i.InteractionTypeId == 1 && i.MyUserId == signedInUser.Id).Any();
				int totalLikes = p.Interactions.Where(i => i.InteractionTypeId == 1).Count();

				bool isRepostedByUser = p.Interactions.Where(i => i.InteractionTypeId == 2 && i.MyUserId == signedInUser.Id).Any();
				int totalReposts = p.Interactions.Where(i => i.InteractionTypeId == 2).Count();

				List<CommentData> comments = new List<CommentData>();
				int totalComments = p.Comments.Count;
				if (totalComments > 0)
				{
					p.Comments.ForEach(c =>
					{
						CommentData commentData = new CommentData()
						{
							username = c.MyUser.UserName,
							imagePath = c.MyUser.ImagePath,
							content = c.Content,
							CreateDate = c.CreateDate.ToLocalTime().ToString("MMM d, yyyy - H:mm")
						};
						comments.Add(commentData);
					});
				}

				postVMs.Add(new PostVM
				{
					PostId = p.Id,
					CreateDate = p.CreateDate.ToLocalTime().ToString("MMM d, yyyy - H:mm"),
					Username = p.MyUser.UserName,
					ImagePath = p.MyUser.ImagePath,
					Title = p.Title,
					Body = p.Body,
					PostTags = postTags,
					isLiked = isLikedByUser,
					totalLikes = totalLikes,
					totalComments = totalComments,
					Comments = comments,
					isReposted = isRepostedByUser,
					totalReposts = totalReposts
				});
			});
			var json = JsonConvert.SerializeObject(postVMs);
			return json;
		}
	}
}
