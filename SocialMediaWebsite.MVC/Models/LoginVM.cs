using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebsite.MVC.Models
{
	public class LoginVM
	{
		[Required]
		[DisplayName("Username")]
		[MaxLength(50, ErrorMessage = "Username can be maximum 50 characters")]
		public string UserName { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[DisplayName("Remember Me")]
		public bool RememberMe { get; set; }
	}
}
