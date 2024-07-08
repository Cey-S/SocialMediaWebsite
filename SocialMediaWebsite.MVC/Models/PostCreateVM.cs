using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SocialMediaWebsite.MVC.Models
{
	public class PostCreateVM
	{
        public string Username { get; set; }
        public string ProfilePicture { get; set; }

        [MaxLength(150, ErrorMessage = "The title can't be longer than 150 characters")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "The title can't be empty")]
		public string Title { get; set; }

		[MaxLength(4000, ErrorMessage = "The body text can't be longer than 4000 characters")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "The body text can't be empty")]
		public string Body { get; set; }

        public List<string> TagNames { get; set; }
    }
}
