using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using SocialMediaWebsite.MVC.Models;

namespace SocialMediaWebsite.MVC.Controllers
{
    public class FeedController : Controller
    {
        private readonly IPostManager postManager;

        public FeedController(IPostManager postManager)
        {
            this.postManager = postManager;
        }

        public IActionResult Index()
        {
            //List<Post> posts = await postManager.GetAllIncludeAsync(null, p => p.Tags, p => p.Owner);
            return View();
        }

        [HttpGet]
        public ActionResult GetData(int pageIndex, int pageSize)
        {
            List<PostVM> postVMs = new List<PostVM>();

            var posts = postManager.SkipAndTakePosts(pageIndex, pageSize).Result;
            posts.ForEach(p =>
            {
				List<string> postTags = new List<string>();
                p.Tags.ForEach(t => { postTags.Add(t.TagName); });

                postVMs.Add(new PostVM
                {
                    PostId = p.Id,
                    Username = p.Owner.UserName,
                    Title = p.Title,
                    Body = p.Body,
                    PostTags = postTags
                });
            });
            var json = JsonConvert.SerializeObject(postVMs);

            return Json(json);
        }
    }
}
