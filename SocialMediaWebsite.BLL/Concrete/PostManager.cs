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

		public async Task<int> DeletePostAsync(int postId, string userId)
		{
			var post = await _posts.AsNoTracking()
						  .Where(p => p.MyUserId == userId && p.Id == postId)
						  .FirstOrDefaultAsync();
			if (post != null)
			{
				return await DeleteAsync(post);
			}

			return 0;
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

		public async Task<List<Post>?> SkipAndTakePopularPosts(int pageIndex, int pageSize)
		{
			return await _posts.AsNoTracking()
				.Include(p => p.Comments)
				.ThenInclude(c => c.MyUser)
				.Include(p => p.MyUser)
				.Include(p => p.Tags)
				.Include(p => p.Interactions)
				.OrderByDescending(p => p.Interactions.Count).ThenByDescending(p => p.Comments.Count).ThenBy(p => p.MyUser.UserName)
				.Skip(pageIndex * pageSize).Take(pageSize)
				.ToListAsync();
		}

		public async Task<List<Post>?> SkipAndTakeFollowingPosts(int pageIndex, int pageSize, int firstPostId, string username)
		{
			if (pageIndex == 0)
			{
				return await _posts.AsNoTracking()
					.Include(p => p.Comments)
					.ThenInclude(c => c.MyUser)
					.Include(p => p.MyUser)
					.Include(p => p.Tags)
					.Include(p => p.Interactions)
					.Where(p => p.MyUser.Followers.Any(u => u.UserName == username))
					.OrderByDescending(p => p.CreateDate)
					.Skip(pageIndex * pageSize).Take(pageSize)
					.ToListAsync();
			}

			return await _posts.AsNoTracking()
					.Include(p => p.Comments)
					.ThenInclude(c => c.MyUser)
					.Include(p => p.MyUser)
					.Include(p => p.Tags)
					.Include(p => p.Interactions)
					.Where(p => p.MyUser.Followers.Any(u => u.UserName == username) && p.Id <= firstPostId)
					.OrderByDescending(p => p.CreateDate)
					.Skip(pageIndex * pageSize).Take(pageSize)
					.ToListAsync();
		}

		public async Task<List<Post>?> SkipAndTakeProfilePosts(int pageIndex, int pageSize, int firstPostId, string username)
		{
			if (pageIndex == 0)
			{
				// The first post Id can be stored on the client side
				return await _posts.AsNoTracking()
					.Include(p => p.MyUser)
					.Include(p => p.Comments).ThenInclude(c => c.MyUser)
					.Include(p => p.Interactions)
					.Include(p => p.Tags)
					.Where(p => p.MyUser.UserName.Equals(username))
					.OrderByDescending(p => p.CreateDate)
					.Skip(pageIndex * pageSize)
					.Take(pageSize)
					.ToListAsync();
			}

			return await _posts.AsNoTracking()
				.Include(p => p.MyUser)
				.Include(p => p.Comments).ThenInclude(c => c.MyUser)
				.Include(p => p.Interactions)
				.Include(p => p.Tags)
				.Where(p => p.MyUser.UserName.Equals(username) && p.Id <= firstPostId)
				.OrderByDescending(p => p.CreateDate)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public async Task<List<Post>?> SkipAndTakeInteractedPosts(int pageIndex, int pageSize, int interactionType, string userId)
		{
			return await _posts.AsNoTracking()
				.Include(p => p.MyUser)
				.Include(p => p.Comments).ThenInclude(c => c.MyUser)
				.Include(p => p.Tags)
				.Include(p => p.Interactions)
				.Where(p => p.Interactions.Any(i => i.InteractionTypeId == interactionType && i.MyUserId == userId))
				.OrderByDescending(p => p.CreateDate)
				.Skip(pageIndex * pageSize)
				.Take(pageSize)
				.ToListAsync();
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
