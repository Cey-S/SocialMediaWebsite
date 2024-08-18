using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	[Authorize(Roles = "AppUser")]
	public class PostController : Controller
	{
		private readonly IPostManager postManager;
		private readonly UserManager<MyUser> userManager;
		private readonly ITagManager tagManager;

		public PostController(IPostManager postManager, UserManager<MyUser> userManager, ITagManager tagManager)
		{
			this.postManager = postManager;
			this.userManager = userManager;
			this.tagManager = tagManager;
		}

		public IActionResult Index()
		{
			var popularTags = tagManager.GetPopularTagCounts();
			ViewBag.PopularTags = popularTags;

			var popularAccounts = userManager.Users
				.Include(p => p.Followers)
				.AsEnumerable()
				.GroupBy(p => p.UserName)
				.Select(g => new
				{
					Username = g.Key == null ? "Error in Getting Username" : g.Key,
					FollowerCount = g.Sum(p => p.Followers.Count)
				})
				.OrderByDescending(x => x.FollowerCount)
				.Take(5)
				.ToDictionary(x => x.Username, x => x.FollowerCount);
			ViewBag.PopularAccounts = popularAccounts;

			return View();
		}

		public IActionResult TagSearch(string? name)
		{
			return View("TagSearch", name);
		}

		public async Task<IActionResult> Delete(int id)
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			var result = await postManager.DeletePostAsync(id, user.Id);

			if(result == 0)
			{
				return BadRequest();
			}

			user.PostCount--;
			await userManager.UpdateAsync(user);

			return RedirectToAction("Profile", "Account");
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			PostCreateVM vM = new PostCreateVM();
			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			vM.Username = user.UserName;
			vM.ProfilePicture = user.ImagePath;

			return View(vM);
		}

		[HttpPost]
		public async Task<IActionResult> Create(PostCreateVM vM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("PostCreate", "Model State is not valid.");
				return View(vM);
			}

			var user = await userManager.Users.Where(p => p.UserName.Equals(vM.Username)).FirstOrDefaultAsync();
			//var user = await userManager.GetAsync(p => p.UserName.Equals(vM.Username));
			if (user == null)
			{
				// This username does not exist in the database
				return View(vM);
			}

			Post post = new Post()
			{
				MyUser = user, // Post owner
				Title = vM.Title,
				Body = vM.Body
			};

			foreach (var item in vM.TagNames)
			{
				var tag = await tagManager.GetAsync(p => p.TagName.Equals(item));

				if (tag != null)
				{
					post.Tags.Add(tag);
				}
				else
				{
					if (!post.Tags.Where(p => p.TagName.Equals(item)).Any())
					{
						Tag newTag = new Tag() { TagName = item };
						post.Tags.Add(newTag);
					}
				}
			}

			var result = await postManager.InsertAsync(post);
			if (result == 0)
			{
				// Could not insert to the database
				return View(vM);
			}

			user.PostCount++;
			await userManager.UpdateAsync(user);

			return RedirectToAction("Index");
		}
	}
}
