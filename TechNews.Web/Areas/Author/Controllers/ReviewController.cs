using System;
using System.Linq;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class ReviewController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        public INotyfService _notyfService { get; }

        public ReviewController(TechNewsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }


        #endregion

        #region Index

        // GET: Reviews
        public ActionResult Index(int itemId, int pageId)
        {
            var reviews = _context.Reviews
                .Where(r => r.ItemId == itemId &&
                            r.Items.AuthorId==int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString())).ToList();


            ViewBag.ItemTitle = _context.Items.Find(itemId).ItemTitle;
            ViewBag.ItemId = itemId;

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(reviews.Count() / (double)take);

            return View(reviews.Skip(skip).Take(take).ToList());
        }

        // GET: Reviews
        public ActionResult AllReviewsIndex(int pageId)
        {
            var reviews = _context.Reviews
                .Where(r=>r.Items.AuthorId== int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()))
                .ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(reviews.Count() / (double)take);

            return View(reviews.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region Details

        // GET: Reviews/Details/5
        public ActionResult Details(int id)
        {
            var reviewDetails = _context.Reviews.FirstOrDefault(r => r.ReviewId == id);

            if (reviewDetails == null)
            {
                return NotFound();
            }

            return View(reviewDetails);
        }


        #endregion

        #region CreateNewAnswer

        // GET: Reviews/Create
        public ActionResult Create(int itemId, int parentId)
        {
            //Getting these two from inputs
            ViewBag.ItemId = itemId;
            ViewBag.ParentId = parentId;

            return View();
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int itemId, int parentId, [Bind("ReviewId", "UserIpAddress", "ReviewTitle", "ReviewDescription", "ReviewDate", "IsPublished", "LikeCount", "DislikeCount", "ParentId", "EmailAddress", "Username", "ItemId")] Review review)
        {
            try
            {

                if (!ModelState.IsValid)
                    return View(review);

                if (itemId != review.ItemId)
                    return NotFound();

                //Specifying required values and adding review to database
                review.UserIpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                review.ReviewDate = DateTime.Now;
                review.LikeCount = 0;
                review.DislikeCount = 0;
                _context.Reviews.Add(review);
                _context.SaveChanges();
                _notyfService.Success("پاسخ شما با موفقیت ثبت شد !");


                return Redirect($"/Admin/Review/Index?itemId={review.ItemId}");
            }
            catch
            {
                _notyfService.Error("مشکلی در ذخیره داده ها رخ داده است ! لطفا با پشتیبانی تماس بگیرید !");
                return View();
            }
        }


        #endregion

        #region UpdateReviewStatus

        // GET: Reviews/Edit/5
        public ActionResult Edit(int id)
        {
            //Getting review details
            var review = _context.Reviews.Find(id);
            if (review == null)
                return NotFound();

            return View(review);
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ReviewId", "UserIpAddress", "ReviewTitle", "ReviewDescription", "ReviewDate", "IsPublished", "LikeCount", "DislikeCount", "ParentId", "EmailAddress", "Username", "ItemId")] Review review)
        {
            try
            {
                //if model was valid then updating review status
                if (!ModelState.IsValid)
                    return View(review);
                if (id != review.ReviewId)
                    return NotFound();

                _context.Reviews.Update(review);
                _context.SaveChanges();
                _notyfService.Success("وضعیت نظر با موفقیت تغییر یافت !");

                return Redirect($"/Admin/Review/Index?itemId={review.ItemId}");
            }
            catch
            {
                _notyfService.Error("مشکلی در ذخیره داده ها رخ داده است ! لطفا با پشتیبانی تماس بگیرید !");
                return View();
            }
        }


        #endregion

        #region RemoveReview

        // GET: Reviews/Delete/5
        public ActionResult Delete(int id)
        {
            var review = _context.Reviews.Find(id);

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //Gathering review details and removing it
                var reviewDetails = _context.Reviews.Find(id);
                _context.Reviews.Remove(reviewDetails);
                _context.SaveChanges();
                _notyfService.Success("کامنت مورد نظر با موفقیت حذف شد !");

                return Redirect($"/Admin/Review/Index?itemId={reviewDetails.ItemId}");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region QuickReviewAccess
        [Route("/Author/UnconfirmedReviews")]
        public IActionResult ReviewQuickConfirmationIndex()
        {
            var review = _context.Reviews
                .Where(r => !r.IsPublished && 
                            r.Items.AuthorId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString())).ToList();

            return View(review);
        }

        public IActionResult ReviewQuickConfirmation(int reviewId)
        {
            var reviewConfirm = _context.Reviews.Find(reviewId);
            if (reviewConfirm != null)
            {
                reviewConfirm.IsPublished = true;
                _context.SaveChanges();
                _notyfService.Success("کامنت با موفقیت تایید شد !");
            }

            return RedirectToAction("ReviewQuickConfirmationIndex");
        }
        public IActionResult QuickDeleteReview(int reviewId)
        {
            var reviewConfirm = _context.Reviews.Find(reviewId);
            if (reviewConfirm != null)
            {
                _context.Reviews.Remove(reviewConfirm);
                _context.SaveChanges();
                _notyfService.Success("کامنت با موفقیت حذف شد !");
            }

            return RedirectToAction("ReviewQuickConfirmationIndex");
        }
        #endregion
    }
}
