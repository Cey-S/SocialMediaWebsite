using Microsoft.AspNetCore.Mvc;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostManager postManager;
		private readonly IManager<AppDbContext, User> userManager;
		private readonly IManager<AppDbContext, Tag> tagManager;

		public PostController(IPostManager postManager, IManager<AppDbContext, User> userManager, IManager<AppDbContext, Tag> tagManager)
		{
			this.postManager = postManager;
			this.userManager = userManager;
			this.tagManager = tagManager;
		}

		//public IActionResult Index()
		//{
		//	return View();
		//}

		[HttpGet]
		public IActionResult Create()
		{
			PostCreateVM vM = new PostCreateVM();
			vM.Username = "veliyilmaz";
			vM.ProfilePicture = "../img/logo/hashtag_128.png";

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

			var user = await userManager.GetAsync(p => p.UserName.Equals(vM.Username));
			if (user == null)
			{
				// This username does not exist in the database
				return View(vM);
			}

			Post post = new Post()
			{
				Owner = user,
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
					Tag newTag = new Tag() { TagName = item };
					post.Tags.Add(newTag);
				}
			}

			var result = await postManager.InsertAsync(post);
			if (result == 0)
			{
				// Could not insert to the database
				return View(vM);
			}

			return RedirectToAction("Index", "Feed");
		}
	}
}
