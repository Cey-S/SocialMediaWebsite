using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Components
{
	public class UserInfoViewComponent(UserManager<MyUser> userManager) : ViewComponent
	{
		private readonly UserManager<MyUser> userManager = userManager;

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			var userRoles = await userManager.GetRolesAsync(user);

			if (user.ImagePath != null) { }

			UserInfoVM vm = new UserInfoVM()
			{
				ProfilePicture = user.ImagePath != null ? user.ImagePath : "../img/logo/hashtag_32.png",
				UserName = user.UserName,
				Role = userRoles.FirstOrDefault()
			};
			return View(vm);
		}
	}
}
