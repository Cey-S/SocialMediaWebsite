using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;

namespace SocialMediaWebsite.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext dbContext = new AppDbContext();

            #region Add new user
            //var user = new User
            //{
            //    Name = "Ayse",
            //    Surname = "Kaya",
            //    UserName = "aysekaya",
            //    Email = "ayse@gmail.com",
            //    Password = "789",                
            //};
            //dbContext.Users.Add(user);
            //var result = dbContext.SaveChanges();
            //System.Console.WriteLine(result + " row affected"); 
            #endregion

            #region User follows another User
            //User ali = dbContext.Users.Where(p => p.UserName.Contains("ali")).FirstOrDefault();
            //User veli = dbContext.Users.Where(p => p.UserName.Contains("veli")).FirstOrDefault();
            //User ayse = dbContext.Users.Where(p => p.UserName.Contains("ayse")).FirstOrDefault();

            //ali.Following.Add(veli); // Ali starts to Follow Veli => Veli gains 1 new Follower
            //ayse.Following.Add(veli); // Ayse starts to Follow Veli => Veli gains 1 new Follower

            //var result = dbContext.SaveChanges();
            //System.Console.WriteLine(result + " row affected"); 
            #endregion

            #region Checking followers
            User ali = dbContext.Users.Where(p => p.UserName.Contains("ali")).Include(p => p.Followers).Include(p => p.Following).FirstOrDefault();
            User veli = dbContext.Users.Where(p => p.UserName.Contains("veli")).Include(p => p.Followers).Include(p => p.Following).FirstOrDefault();
            User ayse = dbContext.Users.Where(p => p.UserName.Contains("ayse")).Include(p => p.Followers).Include(p => p.Following).FirstOrDefault();

            System.Console.WriteLine($"Ali's Follower Count: {ali.Followers.Count}, Following Count: {ali.Following.Count}");
            System.Console.WriteLine($"Veli's Follower Count: {veli.Followers.Count}, Following Count: {veli.Following.Count}");
            System.Console.WriteLine($"Ayse's Follower Count: {ayse.Followers.Count}, Following Count: {ayse.Following.Count}");

            System.Console.WriteLine("Veli's Followers:");
            veli.Followers.ForEach(p => { System.Console.WriteLine($"{p.Name} follows Veli"); });
            #endregion
        }
    }
}
