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
    public class CommentConfig : BaseConfig<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.Content).HasMaxLength(1000);
        }
    }
}
