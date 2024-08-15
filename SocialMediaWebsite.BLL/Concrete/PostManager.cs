﻿using Microsoft.EntityFrameworkCore;
using SocialMediaWebsite.BLL.Abstract;
using SocialMediaWebsite.Core.BusinessLogic;
using SocialMediaWebsite.Entities.DbContexts;
using SocialMediaWebsite.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaWebsite.BLL.Concrete
{
	public class PostManager : Manager<AppDbContext, Post>, IPostManager
	{
		private readonly DbSet<Post> _posts;

		public PostManager(AppDbContext context) : base(context)
		{
			_posts = context.Set<Post>();
		}

		public async Task<List<Post>?> SkipAndTakePosts(int pageIndex, int pageSize, int firstPostId)
		{
			if (pageIndex == 0)
			{
                // The first post Id can be stored on the client side
                return await _posts.OrderByDescending(p => p.CreateDate).Skip(pageIndex * pageSize).Take(pageSize).Include(p => p.MyUser).Include(p => p.Tags).Include(p => p.Interactions).Include(p => p.Comments).ThenInclude(c => c.MyUser).AsNoTracking().ToListAsync();
            }

            return await _posts.OrderByDescending(p => p.CreateDate).Where(p => p.Id <= firstPostId).Skip(pageIndex * pageSize).Take(pageSize).Include(p => p.MyUser).Include(p => p.Tags).Include(p => p.Interactions).Include(p => p.Comments).ThenInclude(c => c.MyUser).AsNoTracking().ToListAsync();
		}

		public async Task<List<Post>?> SkipAndTakeProfilePosts(int pageIndex, int pageSize, int firstPostId, string username)
		{
			if (pageIndex == 0)
			{
				// The first post Id can be stored on the client side
				return await _posts.Where(p => p.MyUser.UserName.Equals(username)).OrderByDescending(p => p.CreateDate).Skip(pageIndex * pageSize).Take(pageSize).Include(p => p.MyUser).Include(p => p.Tags).AsNoTracking().ToListAsync();
			}

			return await _posts.Where(p => p.MyUser.UserName.Equals(username)).OrderByDescending(p => p.CreateDate).Where(p => p.Id <= firstPostId).Skip(pageIndex * pageSize).Take(pageSize).Include(p => p.MyUser).Include(p => p.Tags).AsNoTracking().ToListAsync();

		}

		public async Task<List<Post>?> SkipAndTakePostsWithTag(int pageIndex, int pageSize, int firstPostId, string tag)
		{
			if (pageIndex == 0)
			{
				return await _posts.AsNoTracking()
					.Include(p => p.MyUser)
					.Include(p => p.Comments).ThenInclude(c => c.MyUser)
					.Include(p => p.Interactions)
					.Include(p => p.Tags)
					.Where(p => p.Tags.Any(t => t.TagName == tag))
					.OrderByDescending(p => p.CreateDate)
					.Skip(pageIndex * pageSize).Take(pageSize)
					.ToListAsync();
			}

			return await _posts.AsNoTracking()
					.Include(p => p.MyUser)
					.Include(p => p.Comments).ThenInclude(c => c.MyUser)
					.Include(p => p.Interactions)
					.Include(p => p.Tags)
					.Where(p => p.Tags.Any(t => t.TagName == tag) && p.Id <= firstPostId)
					.OrderByDescending(p => p.CreateDate)
					.Skip(pageIndex * pageSize).Take(pageSize)
					.ToListAsync();
		}
	}
}
