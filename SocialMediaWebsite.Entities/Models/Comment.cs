using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Models
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }

        #region 1-N Relationships
        public int PostId { get; set; }
        public Post Post { get; set; }
        #endregion
    }
}
