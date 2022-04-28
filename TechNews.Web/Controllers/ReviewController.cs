using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using Microsoft.AspNetCore.HttpOverrides;

namespace TechNews.Web.Controllers
{
    public class ReviewController : Controller
    {
        #region Injection

        private IReviewRepository _reviewRepository;
        public INotyfService _notyfService;

        public ReviewController(IReviewRepository reviewRepository, INotyfService notyfService)
        {
            _reviewRepository = reviewRepository;
            _notyfService = notyfService;
        }


        #endregion

        #region AddReview

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(ReviewViewModel review)
        {
            review.UserIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            if (_reviewRepository.AddReview(review))
            {
                _notyfService.Success("نظر شما با موفقیت ثبت شد و پس از تایید مدیریت در سایت نمایش داده خواهد شد !");
            }
            else
            {
                _notyfService.Error("با عرض پوزش ، شما در لیست کاربران غیرمجاز جهت ثبت نظر هستید و نمیتوانید نظر جدیدی ثبت کنید !");
            }

            return Redirect($"/Post/{review.ItemId}");
        }


        #endregion

        #region ReportReview

        public IActionResult ReportReview(int reviewId, int postId)
        {
            _reviewRepository.ReportReview(reviewId);
            _notyfService.Success("دیدگاه برای بررسی مجدد به مدیریت سایت ارسال شد !");
            return Redirect($"/Post/{postId}");
        }


        #endregion

        #region LikeAndDislikeComment

        public IActionResult LikeComment(int reviewId, int postId)
        {
            _reviewRepository.LikeComment(reviewId);
            return Redirect($"/Post/{postId}");
        }
        public IActionResult DislikeComment(int reviewId, int postId)
        {
            _reviewRepository.DislikeComment(reviewId);
            return Redirect($"/Post/{postId}");
        }

        #endregion

    }
}
