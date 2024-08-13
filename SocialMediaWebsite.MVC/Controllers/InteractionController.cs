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
    public class InteractionController(IManager<AppDbContext, Interaction> interactionManager, IManager<AppDbContext, InteractionType> typeManager, UserManager<MyUser> userManager) : ControllerBase
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
    }
}
