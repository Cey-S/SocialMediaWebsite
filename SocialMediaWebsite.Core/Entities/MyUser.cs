using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Core.Entities
{
	public class MyUser : IdentityUser
	{
		public string? ImagePath { get; set; } // Profile Picture

		#region 1-N Relationships
		public List<MyUser>? Followers { get; set; } = new List<MyUser>();
		public List<MyUser>? Followings { get; set; } = new List<MyUser>();
		#endregion
	}
}
