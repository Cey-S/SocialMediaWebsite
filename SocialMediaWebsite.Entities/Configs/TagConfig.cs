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
    public class TagConfig : BaseConfig<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            base.Configure(builder);

            builder.HasIndex(p => p.TagName).IsUnique();
            builder.Property(p => p.TagName).HasMaxLength(50);
        }
    }
}
