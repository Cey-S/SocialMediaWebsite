using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

		// Home Feed Posts
		[HttpGet]
		public async Task<ActionResult> GetData(int pageIndex, int pageSize, int firstPostId)
		{
			List<PostVM> postVMs = new List<PostVM>();

			var posts = await postManager.SkipAndTakePosts(pageIndex, pageSize, firstPostId);
			
			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var signedInUser = await userManager.GetUserAsync(User);

			posts.ForEach(p =>
			{
				List<string> postTags = new List<string>();
				p.Tags.ForEach(t => { postTags.Add(t.TagName); });

                bool isLikedByUser = p.Interactions.Where(i => i.InteractionTypeId == 1 && i.MyUserId == signedInUser.Id).Any();
				int totalLikes = p.Interactions.Where(i => i.InteractionTypeId == 1).Count();

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
							content = c.Content
						};
						comments.Add(commentData);
					});
				}

				postVMs.Add(new PostVM
				{
					PostId = p.Id,
					Username = p.MyUser.UserName,
					ImagePath = p.MyUser.ImagePath,
					Title = p.Title,
					Body = p.Body,
					PostTags = postTags,
					isLiked = isLikedByUser,
					totalLikes = totalLikes,
					totalComments = totalComments,
					Comments = comments
				});
			});
			var json = JsonConvert.SerializeObject(postVMs);

			return Ok(json);
		}

		// User Profile Posts
		[HttpGet]
		public async Task<ActionResult> GetProfileData(int pageIndex, int pageSize, int firstPostId, string username)
		{
			List<PostVM> postVMs = new List<PostVM>();

			var posts = await postManager.SkipAndTakeProfilePosts(pageIndex, pageSize, firstPostId, username);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			posts.ForEach(p =>
			{
				List<string> postTags = new List<string>();
				p.Tags.ForEach(t => { postTags.Add(t.TagName); });

				postVMs.Add(new PostVM
				{
					PostId = p.Id,
					Username = p.MyUser.UserName,
					ImagePath = p.MyUser.ImagePath,
					Title = p.Title,
					Body = p.Body,
					PostTags = postTags
				});
			});
			var json = JsonConvert.SerializeObject(postVMs);

			return Ok(json);
		}

		// Posts with spesific tag name
		[HttpGet]
		public async Task<ActionResult> GetDataWithTag(int pageIndex, int pageSize, int firstPostId, string tag)
		{
			List<PostVM> postVMs = new List<PostVM>();

			var posts = await postManager.SkipAndTakePostsWithTag(pageIndex, pageSize, firstPostId, tag);

			if (posts == null || posts.Count == 0)
			{
				return Ok(null);
			}

			var signedInUser = await userManager.GetUserAsync(User);

			posts.ForEach(p =>
			{
				List<string> postTags = new List<string>();
				p.Tags.ForEach(t => { postTags.Add(t.TagName); });

				bool isLikedByUser = p.Interactions.Where(i => i.InteractionTypeId == 1 && i.MyUserId == signedInUser.Id).Any();
				int totalLikes = p.Interactions.Where(i => i.InteractionTypeId == 1).Count();

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
							content = c.Content
						};
						comments.Add(commentData);
					});
				}

				postVMs.Add(new PostVM
				{
					PostId = p.Id,
					Username = p.MyUser.UserName,
					ImagePath = p.MyUser.ImagePath,
					Title = p.Title,
					Body = p.Body,
					PostTags = postTags,
					isLiked = isLikedByUser,
					totalLikes = totalLikes,
					totalComments = totalComments,
					Comments = comments
				});
			});
			var json = JsonConvert.SerializeObject(postVMs);

			return Ok(json);
		}
	}
}
