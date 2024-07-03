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
    public class UserConfig : BaseConfig<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(p => p.UserName).IsUnique();
            builder.Property(p => p.UserName).HasMaxLength(30);

            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property(p => p.Email).HasMaxLength(100);

            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Surname).HasMaxLength(50);
            builder.Property(p => p.Password).HasMaxLength(100);
            builder.Property(p => p.ImagePath).HasMaxLength(500);
        }
    }
}
