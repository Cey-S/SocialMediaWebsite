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
    public class PhotoConfig : BaseConfig<Photo>
    {
        public override void Configure(EntityTypeBuilder<Photo> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.PhotoPath).HasMaxLength(500);
            builder.HasIndex(p => p.PhotoPath).IsUnique();
        }
    }
}
