using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TechNews.DataLayer.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "نام کاربر")]
        public string UserName { get; set; }
        [Required]
        [MaxLength(250)]
        [EmailAddress(ErrorMessage = "آدرس ایمیل وارد شده معتبر نمی باشد !")]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "تلفن تماس")]
        public string PhoneNumber { get; set; }
        [MaxLength(250)]
        [Display(Name = "تصویر پروفایل")]
        public string ProfilePhoto { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "آدرس آیپی")]
        public string IpAddress { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        [Display(Name = "وضعیت فعال بودن حساب کاربری")]
        public bool IsActive { get; set; }
        [MaxLength(50)]
        [Display(Name = "کد شناسایی کاربر")]
        public string IdentifierCode { get; set; }
        [Required]
        [Display(Name = "سطح دسترسی")]
        public int RoleId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        //Nav
        public List<Role> Role { get; set; }
    }
}
