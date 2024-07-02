using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.Configs
{
    public class InteractionConfig : BaseConfig<Interaction>
    {
        public override void Configure(EntityTypeBuilder<Interaction> builder)
        {
            base.Configure(builder);

            // A User may interact with a Post only once with the specified Interaction Type
            // e.g., User 1 can Like (interaction type) Post 1 only once
            builder.HasIndex(p => new {p.UserId, p.PostId, p.InteractionTypeId}).IsUnique();
        }
    }
}
