using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechNews.DataLayer.Entities
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "عنوان پست")]
        public string ItemTitle { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیح مختصر")]
        [MaxLength(800)]
        public string ShortDescription { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "متن پست")]
        public string ItemDescription { get; set; }
        [MaxLength(250)]
        [Display(Name = "تصویر پست")]
        public string ItemImage { get; set; }
        [Required]
        [Display(Name = "تعداد موردپسند ها")]
        public int LikeCount { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "نویسنده پست")]
        public string ItemAuthor { get; set; }
        [Required]
        [Display(Name = "تاریخ انتشار")]
        public DateTime ItemSubmitDate { get; set; }
        public int? TagId { get; set; }
        public int? ReviewId { get; set; }
        public int? AuthorId { get; set; }

        //Nav
        public List<Tag> Tags { get; set; }
        public List<SelectedCategory> SelectedCategory { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
