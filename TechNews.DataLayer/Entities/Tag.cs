using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechNews.DataLayer.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "عنوان تگ")]
        public string TagTitle { get; set; }
        [Required]
        [Display(Name = "مربوط به پست")]
        public int ItemId { get; set; }

        //Nav
        [ForeignKey("ItemId")]
        public Item Items { get; set; }
    }
}
