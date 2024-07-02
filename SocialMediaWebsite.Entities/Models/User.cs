using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; } // Profile Picture

        #region 1-N Relationships
        public List<Post>? Posts { get; set; }
        public List<Interaction>? Interactions { get; set; }
        public List<User>? Followers { get; set; }
        public List<User>? Following { get; set; }
        #endregion
    }
}
