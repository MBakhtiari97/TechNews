using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.Core.DTOs
{
    public class ReviewViewModel
    {
        public int? ReviewId { get; set; }
        public int ItemId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage ="لطفا {0} خود را وارد کنید")]
        public string Username { get; set; }
        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string EmailAddress { get; set; }
        [Display(Name = "عنوان دیدگاه")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "شرح دیدگاه")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string Description { get; set; }
        public int? LikeCount { get; set; }
        public int? DislikeCount { get; set; }
        public int? ParentId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string UserIpAddress { get; set; }
    }
}
