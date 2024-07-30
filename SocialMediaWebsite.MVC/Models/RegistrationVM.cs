using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaWebsite.MVC.Models
{
	public class RegistrationVM
	{
		[Required]
		[DisplayName("Username")]
		[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
		[MaxLength(50, ErrorMessage = "Username can be maximum 50 characters")]
		public string UserName { get; set; }

		[Required]
		[EmailAddress(ErrorMessage = "Incorrect email format")]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[MinLength(3, ErrorMessage = "Password must be minimum 3 characters")]
		public string Password { get; set; }

		[Required]
		[DisplayName("Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
	}
}
