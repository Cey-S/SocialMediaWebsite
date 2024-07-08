using Microsoft.AspNetCore.Mvc;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	public class PostController : Controller
	{
		private readonly IPostManager postManager;

		public PostController(IPostManager postManager)
        {
			this.postManager = postManager;
		}

		//public IActionResult Index()
		//{
		//	return View();
		//}

		[HttpGet]
        public IActionResult Create()
		{
			PostCreateVM vM = new PostCreateVM();
			vM.Username = "Username";
			vM.ProfilePicture = "../img/logo/hashtag_128.png";

			return View(vM);
		}

		//[HttpPost]
		//public IActionResult Create()
		//{

		//	return View();
		//}
	}
}
