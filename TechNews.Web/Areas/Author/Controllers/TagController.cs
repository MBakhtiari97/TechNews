using System.Collections.Generic;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;

namespace TechNews.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class TagController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        public INotyfService _notyfService;

        public TagController(TechNewsContext context,INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        #endregion

        #region Index

        // GET: Tag
        public ActionResult Index(int itemId)
        {
            var tags = _context.Tags
                .Where(t => t.ItemId == itemId).ToList();

            ViewBag.itemTitle = _context.Items.Find(itemId).ItemTitle;
            ViewBag.itemId = itemId;
            return View(tags);
        }

        #endregion

        #region CreateNewTag

        // GET: Tag/Create
        public ActionResult Create(int itemId)
        {
            ViewBag.ItemId = itemId;
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int itemId, [Bind("TagId", "TagTitle", "ItemId")] DataLayer.Entities.Tag tags)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(tags);

                //Split input value  
                string[] allTags = tags.TagTitle.Split(',');

                //Add each one as a new tag
                foreach (var tag in allTags)
                {
                    var newTag = new DataLayer.Entities.Tag()
                    {
                        ItemId = tags.ItemId,
                        TagTitle = tag
                    };
                    _context.Tags.Add(newTag);
                }
                _context.SaveChanges();
                _notyfService.Success("تگ ها با موفقیت اضافه شدند !");
                return RedirectToAction("Index","Post");
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region RemovingTag

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            var tag = _context.Tags.Find(id);
            return View(tag);
        }

        // POST: Tag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var tag = _context.Tags.Find(id);
                _context.Tags.Remove(tag);
                _context.SaveChanges();
                _notyfService.Success("تگ ها با موفقیت حذف شدند !");

                return RedirectToAction("Index", "Post");
            }
            catch
            {
                return View();
            }
        }

        #endregion

    }
}
