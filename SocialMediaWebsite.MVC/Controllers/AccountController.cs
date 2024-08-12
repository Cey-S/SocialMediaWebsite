using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	[Authorize(Roles = "AppUser")]
	public class AccountController(UserManager<MyUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<MyUser> signInManager, AppDbContext dbContext) : Controller
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
			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			SettingsVM vm = new SettingsVM()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Username = user.UserName,
				Email = user.Email,
				Phone = user.PhoneNumber
			};

			return View(vm);
		}

		[HttpPost]
		public async Task<IActionResult> Settings(SettingsVM vM)
		{
			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			// Model state, username, email validation
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("UpdateProfile", "Model State is not valid.");
			}

			if (!vM.Username.Equals(user.UserName))
			{
				var usernameExists = await userManager.Users.AnyAsync(p => p.UserName == vM.Username);
				if (usernameExists)
				{
					ModelState.AddModelError("UsernameExists", "This username is taken. Please pick another one.");
				}
				else
				{
					// Update username
					user.UserName = vM.Username;
				}
			}

			if (!vM.Email.Equals(user.Email))
			{
				var emailExists = await userManager.Users.AnyAsync(p => p.Email == vM.Email);
				if (emailExists)
				{
					ModelState.AddModelError("EmailExists", "There is already an account associated with this email address.");
				}
				else
				{
					// Update email
					user.Email = vM.Email;
				}
			}

			if (ModelState.ErrorCount > 0)
			{
				return View(vM);
			}

			// Update phone number
			if (vM.Phone != user.PhoneNumber)
			{
				user.PhoneNumber = vM.Phone;
			}

			// Update first name
			if (vM.FirstName != user.FirstName)
			{
				user.FirstName = vM.FirstName;
			}

			// Update last name
			if (vM.LastName != user.LastName)
			{
				user.LastName = vM.LastName;
			}

			// Save profile picture
			if (vM.FormFile != null)
			{
				var extent = Path.GetExtension(vM.FormFile.FileName);
				var newFileName = ($"profile_picture{extent}");

				var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\user-uploads", $"{user.Id}");
				if (!Directory.Exists(folderPath))
				{
					Directory.CreateDirectory(folderPath);
				}

				var imgPath = Path.Combine(folderPath, newFileName);

				using (var stream = new FileStream(imgPath, FileMode.Create))
				{
					await vM.FormFile.CopyToAsync(stream);
				}

				// Update profile picture
				user.ImagePath = $"../user-uploads/{user.Id}/{newFileName}";
			}

			// Update user
			var result = await userManager.UpdateAsync(user);
			if (!result.Succeeded)
			{
				ModelState.AddModelError("UpdateProfile", $"Could not update profile. {result.Errors.First().Description}");
				return View(vM);
			}

			await signInManager.RefreshSignInAsync(user);

			return RedirectToAction("Profile");
		}

		public async Task<IActionResult> Profile(string? username)
		{
			if (username == null || username.Equals(User.Identity.Name))
			{
				var signedInUser = await userManager.GetUserAsync(User);
				if (signedInUser == null)
				{
					return NotFound($"Unable to load user with username '{username}'.");
				}

				ViewBag.isSignedInUser = true;
				return View("Profile", signedInUser);
			}
			else
			{
				var user = await userManager.FindByNameAsync(username);
				if (user == null)
				{
					return NotFound($"Unable to load user with username '{username}'.");
				}

				//var signedInUser = await userManager.GetUserAsync(User);
				//AppDbContext dbContext = new AppDbContext();
				var signedInUser = await dbContext.Users.Where(p => p.UserName == User.Identity.Name).Include(p => p.Followings).AsNoTracking().FirstOrDefaultAsync();

				if (signedInUser == null)
				{
					return NotFound($"Unable to load user with username '{username}'.");
				}

				ViewBag.isSignedInUser = false;
				ViewBag.isFollowed = signedInUser.Followings.Any(p => p.UserName.Equals(username));
				return View("Profile", user);
			}
		}

		public async Task<IActionResult> Follow(string username)
		{
			var user = await userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			var userToFollow = await userManager.FindByNameAsync(username);
			if (userToFollow == null)
			{
				return NotFound($"Unable to load user with username '{username}'.");
			}

			user.Followings.Add(userToFollow);
			user.FollowingCount++;
			var result1 = await userManager.UpdateAsync(user);
			if (!result1.Succeeded)
			{
				return Content($"Could not follow user. {user.UserName} tried to follow {userToFollow.UserName}. {result1.Errors.First()}");
			}

			userToFollow.FollowerCount++;
			var result2 = await userManager.UpdateAsync(userToFollow);
			if (!result2.Succeeded)
			{
				return Content($"Could not follow user. {user.UserName} tried to follow {userToFollow.UserName}. {result2.Errors.First()}");
			}

			return RedirectToAction("Profile", routeValues: new { username });
		}

		public async Task<IActionResult> Unfollow(string username)
		{
			var user = await dbContext.Users.Where(p => p.UserName == User.Identity.Name).Include(p => p.Followings).FirstOrDefaultAsync();
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
			}

			var userToUnfollow = await userManager.FindByNameAsync(username);
			if (userToUnfollow == null)
			{
				return NotFound($"Unable to load user with username '{username}'.");
			}

			user.Followings.Remove(userToUnfollow);
			user.FollowingCount--;
			var result1 = await userManager.UpdateAsync(user);
			if (!result1.Succeeded)
			{
				return Content($"Could not unfollow user. {user.UserName} tried to unfollow {userToUnfollow.UserName}. {result1.Errors.First()}");
			}

			userToUnfollow.FollowerCount--;
			var result2 = await userManager.UpdateAsync(userToUnfollow);
			if (!result2.Succeeded)
			{
				return Content($"Could not unfollow user. {user.UserName} tried to unfollow {userToUnfollow.UserName}. {result2.Errors.First()}");
			}

			return RedirectToAction("Profile", routeValues: new { username });
		}

		public async Task<IActionResult> Followers(string username)
		{
			//var query2 = dbContext.Users.AsNoTracking().Where(p => p.UserName == User.Identity.Name).Select(p => p.Followers);
			//var followers2 = await query2.FirstOrDefaultAsync();

			// Select only the username and profile picture of the followers
			var query = dbContext.Users
				.AsNoTracking()
				.Where(p => p.UserName == username)
				.SelectMany(p => p.Followers,
				(parent, child) => new FollowersVM()
				{
					Username = child.UserName,
					ImagePath = child.ImagePath
				});
			var followers = await query.ToListAsync();

			ViewBag.profileOwner = username;
			return View(followers);
		}

		public async Task<IActionResult> Followings(string username)
		{		
			// Select only the username and profile picture of the followings
			var query = dbContext.Users
				.AsNoTracking()
				.Where(p => p.UserName == username)
				.SelectMany(p => p.Followings,
				(parent, child) => new FollowersVM()
				{
					Username = child.UserName,
					ImagePath = child.ImagePath
				});
			var followings = await query.ToListAsync();

			ViewBag.profileOwner = username;
			return View(followings);
		}
	}
}
