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
    public class InteractionTypeConfig : BaseConfig<InteractionType>
    {
        public override void Configure(EntityTypeBuilder<InteractionType> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Name).HasMaxLength(25);
            builder.HasIndex(p => p.Name).IsUnique();

            builder.HasData(new InteractionType() { Id = 1, Name = "Like", CreateDate = DateTime.UtcNow },
                new InteractionType() { Id = 2, Name = "Share", CreateDate = DateTime.UtcNow });
        }
    }
}
