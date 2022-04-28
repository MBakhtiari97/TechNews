using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.Core.DTOs
{
    public class ShowPostInSearch
    {
        public int ItemId { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDescription { get; set; }
        public DateTime PublishDate { get; set; }
        public int ReviewsCount { get; set; }
        public string ImageTitle { get; set; }
        public string ShortDescription { get; set; }
    }
}
