using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	[Authorize(Roles = "AppUser")]
	public class AccountController(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<MyUser> signInManager) : Controller
	{
		[AllowAnonymous]
		public IActionResult Register()
		{
			RegistrationVM vm = new RegistrationVM();
			return View(vm);
		}

		[HttpPost]
		[AllowAnonymous]
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
				ImagePath = "../img/profile/user_128.png"
			};

			var result = await userManager.CreateAsync(user, vM.Password);
			if (!result.Succeeded)
			{
				// Could not create user
				ModelState.AddModelError("UserCreate", $"Could not create user. {result.Errors.First().Description}");
				return View(vM);
			}

			// If role does not exists, then create role
			if (!await roleManager.RoleExistsAsync("AppUser"))
			{
				var roleResult = await roleManager.CreateAsync(new IdentityRole() { Name = "AppUser" });
				if (!roleResult.Succeeded)
				{
					// Could not create role
					ModelState.AddModelError("UserCreate", $"Could not create user. {result.Errors.First().Description}");
					return View(vM);
				}
			}

			// Add user role
			var userResult = await userManager.AddToRoleAsync(user, "AppUser");
			if (!userResult.Succeeded)
			{
				// Could not add role to user
				ModelState.AddModelError("UserCreate", $"Could not create user. {result.Errors.First().Description}");
				return View(vM);
			}

			return RedirectToAction("Index", "Post");
		}

		[AllowAnonymous]
		public IActionResult Login()
		{
			LoginVM vm = new LoginVM();
			return View(vm);
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login(LoginVM vM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("AccountLogin", "Model State is not valid.");
				return View(vM);
			}

			var result = await signInManager.PasswordSignInAsync(vM.UserName, vM.Password, vM.RememberMe, lockoutOnFailure: false);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("InvalidLogin", "Invalid login attempt. The email or password is incorrect.");
				return View(vM);
			}

			return RedirectToAction("Index", "Post");
		}

		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}

		public async Task<IActionResult> Settings()
		{
			var user = await userManager.FindByNameAsync(User.Identity.Name);
			SettingsVM vm = new SettingsVM()
			{
				UserID = user.Id,
				FirstName = null,
				LastName = null,
				Username = user.UserName,
				Email = user.Email,
				Phone = user.PhoneNumber
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Settings(SettingsVM vM)
		{
			var user = await userManager.FindByIdAsync(vM.UserID);
			if (user == null)
			{
				ModelState.AddModelError("UpdateProfile", "Could not find user.");
				return View();
			}

			// Save profile picture
			if (vM.FormFile != null)
			{
				var extent = Path.GetExtension(vM.FormFile.FileName);
				var newFileName = ($"profile_picture{extent}");

				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\user-uploads", $"{vM.UserID}");
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				var imgPath = Path.Combine(folderPath, newFileName);

				using (var stream = new FileStream(imgPath, FileMode.Create))
				{
					await vM.FormFile.CopyToAsync(stream);
				}

				user.ImagePath = $"../user-uploads/{vM.UserID}/{newFileName}";
			}

			// Model state, username, email validation
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("UpdateProfile", "Model State is not valid.");
			}

			//if (!vM.Username.Equals(user.UserName))
			//{
			//	var usernameExists = await userManager.Users.AnyAsync(p => p.UserName == vM.Username);
			//	if (usernameExists)
			//	{
			//		ModelState.AddModelError("UsernameExists", "This username is taken. Please pick another one.");
			//	}
			//}

			//if (!vM.Email.Equals(user.Email))
			//{
			//	var emailExists = await userManager.Users.AnyAsync(p => p.Email == vM.Email);
			//	if (emailExists)
			//	{
			//		ModelState.AddModelError("EmailExists", "There is already an account associated with this email address.");
			//	}
			//}

			//if (ModelState.ErrorCount > 0)
			//{
			//	return View();
			//}

			//// Update account information
			////user.UserName = vM.Username;
			////user.Email = vM.Email;
			//user.PhoneNumber = vM.Phone;

			var result = await userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("UpdateProfile", $"Could not update profile. {result.Errors.First().Description}");
				return View();
			}

			return RedirectToAction("Index", "Post");
		}
	}
}
