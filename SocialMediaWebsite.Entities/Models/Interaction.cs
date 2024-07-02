using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Models
{
    public class Interaction : BaseEntity
    {
        #region 1-N Relationships
        public int InteractionTypeId { get; set; }
        public InteractionType InteractionType { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } 
        #endregion
    }
}
