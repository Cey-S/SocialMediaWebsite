using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Models
{
    public class Tag : BaseEntity
    {
        public string TagName { get; set; }

        #region N-N Relationships
        public List<Post>? Posts { get; set; } = new List<Post>();
        #endregion
    }
}
