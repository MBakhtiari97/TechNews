using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.DataLayer.Entities
{
    public class Browser
    {
        [Key]
        public int BrowserId { get; set; }
        [Required]
        [MaxLength(250)]
        public string BrowserName { get; set; }
        [Required]
        public int UsedCount { get; set; }
    }
}
