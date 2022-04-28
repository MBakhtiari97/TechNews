using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class Ticket
    {
        [Key]
        [Display(Name = "شماره تیکت")]
        public int TicketId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "نام")]
        public string UserFullName { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "آدرس آیپی")]
        public string UserIpAddress { get; set; }
        [Required]
        [MaxLength(250)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "آدرس ایمیل")]
        public string UserEmailAddress { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "شماره تلفن")]
        public string UserPhoneNumber { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "عنوان تیکت")]
        public string TicketTitle { get; set; }
        [Required]
        [Display(Name = "شرح")]
        public string TicketDescription { get; set; }
        [Required]
        [Display(Name = "تاریخ ثبت")]
        public DateTime TicketSubmitDate { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "کد یکتای تیکت")]
        public string TicketUniqueCode { get; set; }
        [Display(Name = "وضعیت پاسخ دهی")]
        public bool IsAnswered { get; set; }
        [Display(Name = "شرح پاسخ")]
        [DataType(DataType.MultilineText)]
        public string? AnswerDescription { get; set; }
        [Display(Name = "تاریخ پاسخ دهی")]
        public DateTime? AnswerSubmitDate { get; set; }
    }
}
