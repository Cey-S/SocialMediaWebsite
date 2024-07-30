using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebsite.MVC.Models
{
	public class SettingsVM
	{
		[MaxLength(50, ErrorMessage = "Name can be maximum 50 characters")]
        public string? FirstName { get; set; }
		[MaxLength(50, ErrorMessage = "Surname can be maximum 50 characters")]
		public string? LastName { get; set; }

		[Required]
		[DisplayName("Username")]
		[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
		[MaxLength(50, ErrorMessage = "Username can be maximum 50 characters")]
		public string Username { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Incorrect email format")]
		public string Email { get; set; }
        public string? Phone { get; set; }

        public IFormFile? FormFile { get; set; }
    }
}
