using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.Core.Entities;
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
			//var user1 = new MyUser
			//{
			//    UserName = "aliyilmaz"
			//};
			//var user2 = new MyUser
			//{
			//    UserName = "veliyilmaz"
			//};
			//var user3 = new MyUser
			//{
			//    UserName = "aysekaya"
			//};
			//dbContext.Users.Add(user1);
			//dbContext.Users.Add(user2);
			//dbContext.Users.Add(user3);
			//var result = dbContext.SaveChanges();
			//System.Console.WriteLine(result + " row affected");
			#endregion

			#region User follows another User
			//MyUser ali = dbContext.Users.Where(p => p.UserName.Contains("ali")).FirstOrDefault();
			//MyUser veli = dbContext.Users.Where(p => p.UserName.Contains("veli")).FirstOrDefault();
			//MyUser ayse = dbContext.Users.Where(p => p.UserName.Contains("ayse")).FirstOrDefault();

			//// Adding a relationship via Following list
			//ali.Followings.Add(veli); // Ali starts to Follow Veli => Veli gains 1 new Follower
			//ayse.Followings.Add(veli); // Ayse starts to Follow Veli => Veli gains 1 new Follower

			//// Adding a relationship via Followers list
			//ali.Followers.Add(veli); // Veli starts to Follow Ali => Ali gains 1 new Follower

			//var result = dbContext.SaveChanges();
			//System.Console.WriteLine(result + " row affected");
			#endregion

			#region Checking followers
			//MyUser ali = dbContext.Users.Where(p => p.UserName.Contains("ali")).Include(p => p.Followers).Include(p => p.Followings).FirstOrDefault();
			//MyUser veli = dbContext.Users.Where(p => p.UserName.Contains("veli")).Include(p => p.Followers).Include(p => p.Followings).FirstOrDefault();
			//MyUser ayse = dbContext.Users.Where(p => p.UserName.Contains("ayse")).Include(p => p.Followers).Include(p => p.Followings).FirstOrDefault();

			//System.Console.WriteLine($"Ali's Follower Count: {ali.Followers.Count}, Following Count: {ali.Followings.Count}");
			//System.Console.WriteLine($"Veli's Follower Count: {veli.Followers.Count}, Following Count: {veli.Followings.Count}");
			//System.Console.WriteLine($"Ayse's Follower Count: {ayse.Followers.Count}, Following Count: {ayse.Followings.Count}");

			//System.Console.WriteLine("-- Veli's Followers:");
			//veli.Followers.ForEach(p => { System.Console.WriteLine($"{p.UserName} follows Veli"); });
			//System.Console.WriteLine("-- Veli's Followings:");
			//veli.Followings.ForEach(p => { System.Console.WriteLine($"Veli follows {p.UserName}"); });
			#endregion

			#region Add posts and tags
			//User ali = dbContext.Users.Where(p => p.UserName.Contains("ali")).FirstOrDefault();
			//User veli = dbContext.Users.Where(p => p.UserName.Contains("veli")).FirstOrDefault();
			//User ayse = dbContext.Users.Where(p => p.UserName.Contains("ayse")).FirstOrDefault();

			//Post post1 = new Post
			//{
			//    Title = "Lorem Ipsum",
			//    Body = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
			//    "Cras bibendum molestie metus eu pulvinar. Vestibulum ante ipsum " +
			//    "primis in faucibus orci luctus et ultrices posuere cubilia curae; " +
			//    "Praesent ac ipsum vehicula, pretium metus et, tincidunt ex. Donec " +
			//    "eu mauris sapien. Pellentesque urna mi, luctus eu dolor quis, " +
			//    "interdum euismod mauris. Sed ultrices enim metus, id venenatis augue " +
			//    "pellentesque lobortis. Pellentesque commodo mattis massa. Suspendisse " +
			//    "neque nunc, interdum a tincidunt et, tincidunt id risus. Praesent " +
			//    "volutpat lacus et enim hendrerit, ac interdum massa pellentesque. " +
			//    "Sed feugiat, quam ut faucibus suscipit, sem mi ultricies orci, in blandit " +
			//    "metus erat volutpat eros. Nunc tortor libero, molestie quis elementum et, " +
			//    "venenatis in mauris. Aenean imperdiet leo a massa sagittis tincidunt. " +
			//    "Suspendisse porttitor ligula id felis lacinia luctus id nec nulla.",
			//    Owner = ali
			//};

			//Post post2 = new Post
			//{
			//    Title = "Lorem Ipsum",
			//    Body = "Vestibulum gravida fringilla arcu et congue. In rhoncus, ipsum " +
			//    "posuere pretium malesuada, odio tellus faucibus felis, vitae ornare nibh " +
			//    "velit vel dui. Pellentesque lobortis vulputate est, scelerisque molestie " +
			//    "nisi faucibus sit amet. Pellentesque ullamcorper, quam nec semper volutpat, " +
			//    "sapien massa consectetur turpis, vitae aliquet turpis nisl nec erat. " +
			//    "Donec non dolor ligula. Aenean eget sem eu sem porttitor luctus. Duis " +
			//    "dignissim, justo mollis fermentum faucibus, erat nisl volutpat quam, sit " +
			//    "amet pretium lorem massa id diam. Quisque consequat massa quis suscipit " +
			//    "molestie. Nam in nibh orci. Mauris non semper eros, at volutpat quam. " +
			//    "Nulla malesuada eget nisl eget facilisis. Morbi pulvinar orci ut ipsum " +
			//    "aliquet, in viverra justo malesuada. Proin eleifend dui sit amet metus " +
			//    "pulvinar, in sollicitudin nisi tincidunt.",
			//    Owner = ali
			//};

			//Post post3 = new Post
			//{
			//    Title = "Lorem Ipsum",
			//    Body = "Aliquam sit amet nunc a nisl tincidunt venenatis. Vestibulum lorem " +
			//    "erat, gravida a lobortis in, semper et tortor. Nullam vel posuere libero. " +
			//    "Suspendisse mollis est sit amet augue pharetra, sit amet ultricies enim " +
			//    "luctus. Pellentesque euismod lorem augue, vel suscipit lacus consectetur " +
			//    "faucibus. Sed ullamcorper lectus fringilla lorem dictum laoreet. " +
			//    "Suspendisse tincidunt auctor varius. Morbi molestie tellus non tortor " +
			//    "cursus molestie. Pellentesque habitant morbi tristique senectus et netus " +
			//    "et malesuada fames ac turpis egestas. Nulla facilisi. Nullam consequat " +
			//    "tempor blandit. Sed lacinia libero eget ex tempus, et feugiat tortor " +
			//    "consequat. Praesent finibus elit lobortis sem porta, eget elementum risus dictum.",
			//    Owner = veli
			//};

			//Post post4 = new Post
			//{
			//    Title = "Lorem Ipsum",
			//    Body = "Pellentesque dolor ipsum, mattis non dictum sit amet, congue id " +
			//    "quam. Mauris dignissim, sem at feugiat volutpat, tellus turpis ornare " +
			//    "risus, nec vehicula augue felis a lacus. Praesent lacinia luctus ex, et " +
			//    "imperdiet tellus facilisis gravida. Mauris diam metus, blandit eu velit " +
			//    "et, tempus dignissim ante. Ut pellentesque accumsan massa, nec dignissim " +
			//    "ante pretium ut. Maecenas tellus mauris, iaculis ac cursus lobortis, " +
			//    "dignissim nec lorem. Nullam arcu nisl, mattis a tortor sed, auctor " +
			//    "fringilla nisi. In sed urna sed nunc eleifend suscipit.",
			//    Owner = ayse
			//};

			//Post post5 = new Post
			//{
			//    Title = "Lorem Ipsum",
			//    Body = "In vulputate pulvinar risus et pharetra. Curabitur ac nunc vel est " +
			//    "sodales feugiat et ut orci. Duis suscipit dui eu aliquet porta. Aliquam " +
			//    "neque odio, mattis a ante ut, congue dictum lectus. Sed vitae turpis dui. " +
			//    "Curabitur aliquet orci eget augue condimentum, ullamcorper mollis ligula " +
			//    "placerat. Vivamus nec elit luctus, molestie diam a, ultricies eros. Mauris " +
			//    "commodo purus arcu, porttitor volutpat turpis sodales vitae. Integer mollis " +
			//    "magna in lorem sollicitudin vulputate. Aliquam interdum eros a porttitor " +
			//    "egestas. Fusce id dictum leo.",
			//    Owner = ayse
			//};

			//Tag tag1 = new Tag { TagName = "Morbi" };
			//Tag tag2 = new Tag { TagName = "Pharetra" };
			//Tag tag3 = new Tag { TagName = "Dignissim" };
			//Tag tag4 = new Tag { TagName = "Fringilla" };
			//Tag tag5 = new Tag { TagName = "Aliquam" };
			//Tag tag6 = new Tag { TagName = "Iaculis" };
			//Tag tag7 = new Tag { TagName = "Rutrum" };
			//Tag tag8 = new Tag { TagName = "Sapien" };

			//post1.Tags.Add(tag1);
			//post1.Tags.Add(tag2);
			//post1.Tags.Add(tag3);

			//post2.Tags.Add(tag4);
			//post2.Tags.Add(tag5);

			//post3.Tags.Add(tag6);
			//post3.Tags.Add(tag7);
			//post3.Tags.Add(tag8);

			//post4.Tags.Add(tag5);
			//post4.Tags.Add(tag2);

			//post5.Tags.Add(tag2);
			//post5.Tags.Add(tag3);
			//post5.Tags.Add(tag4);
			//post5.Tags.Add(tag5);

			//dbContext.Posts.Add(post1);
			//dbContext.Posts.Add(post2);
			//dbContext.Posts.Add(post3);
			//dbContext.Posts.Add(post4);
			//dbContext.Posts.Add(post5);

			//var result = dbContext.SaveChanges();
			//System.Console.WriteLine(result + " row affected"); // 27 row affected
			#endregion

			#region Checking posts and tags
			//User ali = dbContext.Users.Where(p => p.UserName.Contains("ali")).Include(p => p.Posts).ThenInclude(p => p.Tags).FirstOrDefault();
			//User veli = dbContext.Users.Where(p => p.UserName.Contains("veli")).Include(p => p.Posts).ThenInclude(p => p.Tags).FirstOrDefault();
			//User ayse = dbContext.Users.Where(p => p.UserName.Contains("ayse")).Include(p => p.Posts).ThenInclude(p => p.Tags).FirstOrDefault();

			#region All Posts
			//ali.Posts.ForEach(p =>
			//    {
			//        System.Console.WriteLine($"{p.Owner.UserName}'s Post: {p.Title}\n\n{p.Body}\n");
			//        p.Tags.ForEach(x => { System.Console.Write($"#{x.TagName} "); });
			//        System.Console.WriteLine("\n-----------------------------------------------\n");
			//    });


			//veli.Posts.ForEach(p =>
			//{
			//    System.Console.WriteLine($"{p.Owner.UserName}'s Post: {p.Title}\n\n{p.Body}\n");
			//    p.Tags.ForEach(x => { System.Console.Write($"#{x.TagName} "); });
			//    System.Console.WriteLine("\n-----------------------------------------------\n");
			//});


			//ayse.Posts.ForEach(p =>
			//{
			//    System.Console.WriteLine($"{p.Owner.UserName}'s Post: {p.Title}\n\n{p.Body}\n");
			//    p.Tags.ForEach(x => { System.Console.Write($"#{x.TagName} "); });
			//    System.Console.WriteLine("\n-----------------------------------------------\n");
			//});
			#endregion

			#region Posts with the tag "Aliquam"
			//Tag specificTag = dbContext.Tags.Where(p => p.TagName.Equals("Aliquam")).Include(p => p.Posts).FirstOrDefault();
			//System.Console.WriteLine($"--> There are {specificTag.Posts.Count} posts in #Aliquam tag:\n");
			//specificTag.Posts.ForEach(p =>
			//{
			//    System.Console.WriteLine($"{p.Owner.UserName}'s Post: {p.Title}\n\n{p.Body}\n");
			//    p.Tags.ForEach(x => { System.Console.Write($"#{x.TagName} "); });
			//    System.Console.WriteLine("\n-----------------------------------------------\n");
			//});
			#endregion
			#endregion
		}
	}
}
