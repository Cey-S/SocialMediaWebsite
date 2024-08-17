using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class InteractionController(IManager<AppDbContext, Interaction> interactionManager,
		IManager<AppDbContext, InteractionType> typeManager,
		IManager<AppDbContext, Comment> commentManager,
		UserManager<MyUser> userManager) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult> Like(int id, int likes)
		{
			var type = await typeManager.GetAsync(p => p.Name.Equals("Like"));
			var user = await userManager.GetUserAsync(User);

			Interaction interaction = new Interaction()
			{
				InteractionType = type,
				MyUser = user,
				PostId = id
			};

			var result = await interactionManager.InsertAsync(interaction);
			if (result > 0)
			{
				int newTotal = likes + 1;
				var json = JsonConvert.SerializeObject(new { id, newTotal });
				return Ok(json);
			}

			return BadRequest();
		}

		[HttpGet]
		public async Task<ActionResult> Unlike(int id, int likes) // remove like
		{
			var type = await typeManager.GetAsync(p => p.Name.Equals("Like"));
			var user = await userManager.GetUserAsync(User);

			var interaction = await interactionManager.GetAsync(p => p.InteractionType == type && p.MyUser == user && p.PostId == id);
			if (interaction == null)
			{
				return BadRequest();
			}

			var result = await interactionManager.DeleteAsync(interaction);
			if (result > 0)
			{
				int newTotal = likes - 1;
				var json = JsonConvert.SerializeObject(new { id, newTotal });
				return Ok(json);
			}

			return BadRequest();
		}

		[HttpGet]
		public async Task<ActionResult> SendComment(int id, int comments, string content)
		{
			var user = await userManager.GetUserAsync(User);

			Comment comment = new Comment()
			{
				MyUser = user,
				Content = content,
				PostId = id
			};

			var result = await commentManager.InsertAsync(comment);
			if (result > 0)
			{
				int newTotal = comments + 1;
				var json = JsonConvert.SerializeObject(new { id, newTotal, content, username = user.UserName, imagePath = user.ImagePath, createTime = DateTime.Now.ToString("MMM d, yyyy - H:mm") });
				return Ok(json);
			}
			return BadRequest();
		}

		[HttpGet]
		public async Task<ActionResult> Repost(int id, int reposts, bool isReposted)
		{
			var type = await typeManager.GetAsync(p => p.Name.Equals("Share"));
			var user = await userManager.GetUserAsync(User);

			if (isReposted)
			{
				var interaction = await interactionManager.GetAsync(p => p.InteractionTypeId == type.Id && p.MyUserId == user.Id && p.PostId == id);
				if (interaction != null)
				{
					var result = await interactionManager.DeleteAsync(interaction);
					if (result > 0)
					{
						isReposted = false;
						int newTotal = reposts - 1;
						var json = JsonConvert.SerializeObject(new { id, newTotal, isReposted });
						return Ok(json);
					}
				}
			}
			else
			{
				Interaction interaction = new Interaction()
				{
					InteractionType = type,
					MyUser = user,
					PostId = id
				};
				var result = await interactionManager.InsertAsync(interaction);
				if (result > 0)
				{
					isReposted = true;
					int newTotal = reposts + 1;
					var json = JsonConvert.SerializeObject(new { id, newTotal, isReposted });
					return Ok(json);
				}
			}

			return BadRequest();
		}

	}
}
