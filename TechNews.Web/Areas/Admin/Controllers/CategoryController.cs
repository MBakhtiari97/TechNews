using System;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        public INotyfService _notyfService;

        public CategoryController(TechNewsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        #endregion

        #region Index

        // GET: CategoryController
        public ActionResult Index(int pageId)
        {
            var categories = _context.Categories.ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(categories.Count() / (double)take);

            return View(categories.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region Details

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }


        #endregion

        #region CreateCategory

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("CategoryId", "CategoryTitle")] Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(category);

                _context.Categories.Add(category);
                _context.SaveChanges();

                _notyfService.Success("دسته بندی مورد نظر با موفقیت افزوده شد!");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyfService.Error("مشکلی پیش آمده است !");
                return View();
            }
        }


        #endregion

        #region UpdateCategory

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("CategoryId", "CategoryTitle")] Category category)
        {
            try
            {
                if (id != category.CategoryId)
                    return NotFound();

                if (!ModelState.IsValid)
                    return View(category);

                _context.Categories.Update(category);
                _context.SaveChanges();
                _notyfService.Success("تغییرات با موفقیت ذخیره شد !");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyfService.Error("مشکلی پیش آمده است !");
                return View();
            }
        }


        #endregion

        #region RemoveCategory

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            return View();
        }

        // POST: CategoryController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var category = _context.Categories.Find(id);

                _context.Categories.Remove(category);
                _context.SaveChanges();

                _notyfService.Success("دسته بندی مورد نظر با موفقیت حذف گردید !");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyfService.Error("مشکلی پیش آمده است !");
                return View();
            }
        }

        #endregion

    }
}
