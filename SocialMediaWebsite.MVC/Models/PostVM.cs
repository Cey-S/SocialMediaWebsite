using SocialMediaWebsite.Entities.Models;

namespace SocialMediaWebsite.MVC.Models
{
    public class PostVM
    {
        public int PostId { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> PostTags { get; set; } = new List<string>();
    }
}
