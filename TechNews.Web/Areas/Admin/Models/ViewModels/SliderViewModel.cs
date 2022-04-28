using Microsoft.AspNetCore.Http;

namespace TechNews.Web.Areas.Admin.Models.ViewModels
{
    public class SliderViewModel
    {
        public int SlideId { get; set; }
        public IFormFile SlideBanner { get; set; }
        public string SlideAlt { get; set; }
        public int ItemId { get; set; }
        public bool IsActive { get; set; }
    }
}
