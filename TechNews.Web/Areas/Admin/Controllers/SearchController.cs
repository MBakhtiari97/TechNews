using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private TechNewsContext _context;

        public SearchController(TechNewsContext context)
        {
            _context = context;
        }

        [Route("Admin/Post/Search")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchInItems(string q, int pageId)
        {
            ViewBag.PhrasedSearched = q;

            var result = _context.Items
                .Where(i =>
                i.ItemTitle.Contains(q) || i.ItemDescription.Contains(q) || i.ShortDescription.Contains(q))
                .ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            return View(result.Skip(skip).Take(take).ToList());
        }

        [Route("Admin/User/Search")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchInUsers(string q, int pageId)
        {
            ViewBag.PhrasedSearched = q;
            var result = _context
                .Users
                .Where(u => u.EmailAddress.Contains(q) || u.UserName.Contains(q) || u.PhoneNumber.Contains(q))
                .ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            return View(result.Skip(skip).Take(take).ToList());
        }
    }
}
