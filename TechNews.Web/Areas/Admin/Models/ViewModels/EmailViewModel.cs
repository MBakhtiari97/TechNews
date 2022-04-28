using System.ComponentModel.DataAnnotations;

namespace TechNews.Web.Areas.Admin.Models.ViewModels
{
    public class CustomEmailViewModel
    {
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [Display(Name = "عنوان ایمیل ارسالی")][MaxLength(250)]
        public string EmailTitle { get; set; }
        [Required(ErrorMessage ="لطفا {0} را وارد کنید")]
        [Display(Name = "شرح ایمیل ارسالی")]
        [DataType(DataType.MultilineText)]
        public string EmailDescription { get; set; }
        [Required]        [EmailAddress(ErrorMessage = "ایمیل وار دشده معتبر نمی باشد")]
        [Display(Name = "آدرس ایمیل دریافت کننده")]
        [MaxLength(250)]
        public string ReceiverEmailAddress { get; set; }
    }
    public class NewsletterEmailViewModel
    {
        [Display(Name = "عنوان ایمیل ارسالی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250)]
        public string EmailTitle { get; set; }
        [Display(Name = "شرح ایمیل ارسالی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string EmailDescription { get; set; }
    }
}
