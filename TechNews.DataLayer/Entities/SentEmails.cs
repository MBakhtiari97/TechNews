using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class SentEmails
    {
        [Key]
        public int EmailId { get; set; }
        [Display(Name = "عنوان ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Title { get; set; }
        [Display(Name = "تاریخ ارسال")]
        public DateTime Date { get; set; }
        [Display(Name = "دریافت کننده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string Receiver { get; set; }
    }
}
