using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	public class AccountController(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager) : Controller
	{
		public IActionResult Register()
		{
			RegistrationVM vm = new RegistrationVM();
			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegistrationVM vM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("AccountRegister", "Model State is not valid.");
			}

			var usernameExists = await userManager.Users.AnyAsync(p => p.UserName == vM.UserName);
			if (usernameExists)
			{
				ModelState.AddModelError("UsernameExists", "This username is taken. Please pick another one.");
			}

			var emailExists = await userManager.Users.AnyAsync(p => p.Email == vM.Email);
			if (emailExists)
			{
				ModelState.AddModelError("EmailExists", "There is already an account associated with this email address.");
			}

			if (ModelState.ErrorCount > 0)
			{
				return View(vM);
			}

			// Create user
			MyUser user = new MyUser()
			{
				UserName = vM.UserName,
				Email = vM.Email,
			};

			var result = await userManager.CreateAsync(user, vM.Password);
			if (!result.Succeeded)
			{
				// Could not create user
				ModelState.AddModelError("UserCreate", "Could not create user.");
				return View(vM);
			}

			// If role does not exists, then create role
			if (!await roleManager.RoleExistsAsync("AppUser"))
			{
				var roleResult = await roleManager.CreateAsync(new IdentityRole() { Name = "AppUser" });
				if (!roleResult.Succeeded)
				{
					// Could not create role
					ModelState.AddModelError("RoleCreate", "Could not create role.");
					return View(vM);
				}
			}

			// Add user role
			var userResult = await userManager.AddToRoleAsync(user, "AppUser");
			if (!userResult.Succeeded)
			{
				// Could not add role to user
				ModelState.AddModelError("UserCreate", "Could not add role to user.");
				return View(vM);
			}

			return RedirectToAction("Index", "Post");
		}
	}
}
