using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.BLL.Concrete;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Core.Entities;
using SocialMediaWebsite.Entities.DbContexts;

namespace SocialMediaWebsite.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(p => p.UseMySQL(builder.Configuration.GetConnectionString("SocialMediaWebsite")));

            builder.Services.AddScoped(typeof(IManager<,>), typeof(Manager<,>));
            builder.Services.AddScoped<IPostManager, PostManager>();

            builder.Services.AddIdentity<MyUser, IdentityRole>(options =>
			{
				options.Password.RequireDigit = false; 
				options.Password.RequireLowercase = false; 
				options.Password.RequireUppercase = false; 
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false; 


				options.User.RequireUniqueEmail = true;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5; 
				options.SignIn.RequireConfirmedPhoneNumber = false;
				options.SignIn.RequireConfirmedEmail = false; 
				options.SignIn.RequireConfirmedAccount = false;
			}).AddEntityFrameworkStores<AppDbContext>()
				.AddDefaultTokenProviders(); 


			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Post}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
