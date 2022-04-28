using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class Category
    {
        [Key]
        [Display(Name = "شماره دسته بندی")]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(250)]
        [Display(Name = "عنوان دسته بندی")]
        public string CategoryTitle { get; set; }

        //Nav
        public List<SelectedCategory> SelectedCategory { get; set; }
    }
}
