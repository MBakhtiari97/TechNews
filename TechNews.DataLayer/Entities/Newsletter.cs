using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class Newsletter
    {
        public int NewsletterId { get; set; }
        [MaxLength(250)]
        [Required]
        [Display(Name = "آدرس ایمیل")]
        public string EmailAddress { get; set; }
    }
}
