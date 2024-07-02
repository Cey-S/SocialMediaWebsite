using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Models
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Body { get; set; }

        #region 1-N Relationships
        public int UserId { get; set; }
        public User Owner { get; set; } // Post Owner

        public List<Photo>? Photos { get; set; }
        public List<Interaction>? Interactions { get; set; }
        public List<Comment>? Comments { get; set; }
        #endregion

        #region N-N Relationships
        public List<Tag>? Tags { get; set; } 
        #endregion
    }
}
