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
        public List<Photo>? Photos { get; set; } = new List<Photo>();
        public List<Interaction>? Interactions { get; set; } = new List<Interaction>();
        public List<Comment>? Comments { get; set; } = new List<Comment>();
        #endregion

        #region N-N Relationships
        public List<Tag>? Tags { get; set; } = new List<Tag>(); 
        #endregion
    }
}
