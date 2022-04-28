using System;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsletterController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        public INotyfService _notyfService;

        public NewsletterController(TechNewsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        #endregion

        #region Index

        public IActionResult Index(int pageId)
        {
            var newsletterSubscribedUsers = _context.Newsletter.ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(newsletterSubscribedUsers.Count() / (double)take);

            return View(newsletterSubscribedUsers.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region RemoveUserFromNewsletter

        public ActionResult Delete(int id)
        {
            var subscribedUser = _context.Newsletter.Find(id);
            return View(subscribedUser);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var subscribedUser = _context.Newsletter.Find(id);
            if (subscribedUser == null)
                return NotFound();

            _context.Newsletter.Remove(subscribedUser);
            _context.SaveChanges();

            _notyfService.Success("کاربر با موفقیت از سرویس خبرنامه حذف شد !");
            return RedirectToAction("Index");
        }

        #endregion

    }
}
