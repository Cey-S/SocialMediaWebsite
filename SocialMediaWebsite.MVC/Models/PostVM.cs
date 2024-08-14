﻿using SocialMediaWebsite.Entities.Models;

namespace SocialMediaWebsite.MVC.Models
{
    public class PostVM
    {
        public int PostId { get; set; }
        public string Username { get; set; }
        public string ImagePath { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> PostTags { get; set; } = new List<string>();
        public bool isLiked { get; set; }
        public int totalLikes { get; set; }
        public int totalComments { get; set; }
        public List<CommentData>? Comments { get; set; }
    }

    public struct CommentData
    {
        public string imagePath { get; set; }
        public string username { get; set; }
        public string content { get; set; }
    }
}
