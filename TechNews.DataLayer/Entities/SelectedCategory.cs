using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class SelectedCategory
    {
        [Key]
        public int SelectedCategoryId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int ItemId { get; set; }

        //Nav
        [ForeignKey("CategoryId")]
        public Category Categories { get; set; }

        [ForeignKey("ItemId")]
        public Item Items { get; set; }
    }
}
