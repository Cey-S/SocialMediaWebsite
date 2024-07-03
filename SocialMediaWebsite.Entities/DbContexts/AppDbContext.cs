using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.Entities.DbContexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<InteractionType> InteractionTypes { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext()
        {
            
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(@"Server=localhost;Database=SocialMediaWebsite;Uid=root;Pwd=Wissen99*;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
