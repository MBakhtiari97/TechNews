using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class Review
    {

        [Key]
        public int ReviewId { get; set; }
        [MaxLength(50)]
        [Display(Name = "آدرس آیپی")]
        public string? UserIpAddress { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "عنوان کامنت")]
        public string ReviewTitle { get; set; }
        [Required]
        [MaxLength(1200)]
        [Display(Name = "شرح کامنت")]
        public string ReviewDescription { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime ReviewDate { get; set; }
        [Required]
        [Display(Name = "وضعیت انتشار")]
        public bool IsPublished { get; set; }
        [Display(Name = "تعداد مورد پسند")]
        public int? LikeCount { get; set; }
        [Display(Name = "تعداد مخالف")]
        public int? DislikeCount { get; set; }
        public int? ParentId { get; set; }
        [MaxLength(250)]
        [Display(Name = "آدرس ایمیل")]
        public string? EmailAddress { get; set; }
        [MaxLength(250)]
        [Display(Name = "نام کاربری")]
        public string? Username { get; set; }
        [Required]
        [Display(Name = "پست")]
        public int ItemId { get; set; }
        //Nav

        [ForeignKey("ItemId")]
        public Item Items { get; set; }
    }
}
