using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TechNews.Web.Areas.Admin.Models.ViewModels
{
    public class PostViewModel
    {
        public int ItemId { get; set; }
        public string ItemTitle { get; set; }
        public string ShortDescription { get; set; }
        public string ItemDescription { get; set; }
        public int LikeCount { get; set; }
        public string ItemAuthor { get; set; }
        public DateTime ItemSubmitDate { get; set; }
        public int TagId { get; set; }
        public int ReviewId { get; set; }
        public IFormFile ItemImage { get; set; }
    }
}
