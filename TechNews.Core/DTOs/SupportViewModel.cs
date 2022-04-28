using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.Core.DTOs
{
    public class TicketViewModel
    {
        [MaxLength(250)]
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string UserFullName { get; set; }
        [MaxLength(250)]
        [EmailAddress(ErrorMessage = "لطفا از آدرس ایمیل معتبر استفاده کنید !")]
        [Display(Name = "پست الکترونیکی")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        public string UserEmailAddress { get; set; }
        [MaxLength(50)]
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "لطفا {0} خود را وارد کنید")]
        [DataType(DataType.PhoneNumber,ErrorMessage = "شماره تلفن نمیتواند حاوی کاراکتر باشد !")]
        public string UserPhoneNumber { get; set; }
        [MaxLength(250)]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TicketTitle { get; set; }
        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string TicketDescription { get; set; }
        public string IpAddress { get; set; }
    }
}
