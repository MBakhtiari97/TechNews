using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.Services.Interfaces;

namespace TechNews.Web.ViewComponents
{
    public class ReviewViewComponent:ViewComponent
    {
        private IReviewRepository _reviewRepository;

        public ReviewViewComponent(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int postId)
        {
            ViewBag.PostId = postId;
            return View("/Views/Shared/_Reviews.cshtml",_reviewRepository.GetReviewsByPostId(postId));
        }
    }
}
