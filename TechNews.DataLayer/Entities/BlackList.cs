using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class BlackList
    {
        [Key]
        [Display(Name = "شماره")]
        public int BlackListId { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "آدرس آیپی")]
        public string BlackListIpAddress { get; set; }
    }
}
