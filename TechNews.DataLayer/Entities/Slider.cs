using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class Slider
    {
        [Key]
        [Display(Name = "شماره اسلاید")]
        public int SlideId { get; set; }
        [MaxLength(250)]
        [Display(Name = "بنر اسلاید")]
        public string SlideBanner { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "عنوان اسلاید")]
        public string SlideAlt { get; set; }
        [Required]
        [Display(Name = "آدرس پست مربوطه")]
        public string ItemRefersTo { get; set; }
        [Required]
        [Display(Name = "وضعیت فعال بودن اسلاید")]
        public bool IsActive { get; set; }
        [Display(Name = "تاریخ ثبت اسلاید")]
        public DateTime SlideSubmitDate { get; set; }

    }
}
