using SocialMediaWebsite.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Models
{
    public class InteractionType : BaseEntity
    {
        public string Name { get; set; } // e.g., like, share

        #region 1-N Relationships
        public List<Interaction>? Interactions { get; set; } 
        #endregion
    }
}
