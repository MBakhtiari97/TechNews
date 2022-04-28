using System;
using System.IO;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;
using TechNews.Utility;
using TechNews.Utility.Generators;
using TechNews.Web.Areas.Admin.Models.ViewModels;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        private IViewRenderService _viewRenderService;
        public INotyfService _notyfService { get; }

        public SliderController(TechNewsContext context, IViewRenderService viewRenderService, INotyfService notyfService)
        {
            _context = context;
            _viewRenderService = viewRenderService;
            _notyfService = notyfService;
        }

        #endregion

        #region BindingProperties

        [BindProperty]
        public SliderViewModel Slider { get; set; }


        #endregion

        #region Index

        // GET: SliderController
        public ActionResult Index(int pageId)
        {
            var slides = _context.Slider.ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(slides.Count() / (double)take);

            return View(slides.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region Details

        // GET: SliderController/Details/5
        public ActionResult Details(int id)
        {
            var slide = _context.Slider.Find(id);
            return View(slide);
        }


        #endregion

        #region CreateNewSlider

        // GET: SliderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("SlideId", "SlideBanner", "SlideAlt", "ItemRefersTo", "IsActive", "SlideSubmitDate")] Slider slider)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(slider);
                //Generating new unique name for banner
                var slideBannerName = StringGenerator.GenerateUniqueString();

                //If banner was selected and it was an image then naming it by the name has generated already
                if (Slider.SlideBanner?.Length > 0 && Slider.SlideBanner.IsImage())
                {
                    slider.SlideBanner = slideBannerName + Path.GetExtension(Slider.SlideBanner.FileName);
                }

                //But if the banner wasn't selected then name it comingsoon.png
                else
                {
                    slider.SlideBanner = "ComingSoon.png";
                }

                //Filling required values and adding slide
                slider.SlideSubmitDate = DateTime.Now;
                _context.Slider.Add(slider);
                _context.SaveChanges();

                //Checking if banner was selected by user (admin) then copying it to server
                if (Slider.SlideBanner?.Length > 0 && Slider.SlideBanner.IsImage())
                {
                    var pathFile = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "slides",
                        slideBannerName +
                        Path.GetExtension(Slider.SlideBanner.FileName));
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        Slider.SlideBanner.CopyTo(stream);
                    }
                }

                _notyfService.Success("اسلاید جدید با موفقیت اضافه شد !");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region UpdateSlider

        // GET: SliderController/Edit/5
        public ActionResult Edit(int id)
        {
            var slideDetails = _context.Slider.Find(id);
            return View(slideDetails);
        }

        // POST: SliderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("SlideId", "SlideBanner", "SlideAlt", "ItemRefersTo", "IsActive", "SlideSubmitDate")] Slider slider)
        {
            try
            {
                if (id != slider.SlideId)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return View(slider);
                }

                //Getting current slide details
                var slideDetails = _context.Slider.AsNoTracking()
                    .FirstOrDefault(s => s.SlideId == id);

                //If image was selected it means admin wants to change the image then , generating a new name an asigning it to banner name
                if (Slider.SlideBanner?.Length > 0 && Slider.SlideBanner.IsImage())
                {
                    var newBanerName = StringGenerator.GenerateUniqueString();
                    slider.SlideBanner = newBanerName + Path.GetExtension(Slider.SlideBanner.FileName);

                    //If the image was selected and it wasn't with the name of comingsoon.png then we should delete it from server
                    if (slideDetails.SlideBanner != "ComingSoon.png")
                    {
                        var oldPath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "img",
                            "slides",
                            slideDetails.SlideBanner);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        var newPath = Path.Combine(Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "img",
                            "slides",
                            newBanerName +
                            Path.GetExtension(Slider.SlideBanner.FileName)
                            );
                        
                        //Copying new image to server
                        using (var stream = new FileStream(newPath, FileMode.Create))
                        {
                            Slider.SlideBanner.CopyTo(stream);
                        }
                    }
                }
                else
                {
                    slider.SlideBanner = slideDetails.SlideBanner;
                }

                //Updating slide details
                _context.Update(slider);
                _context.SaveChanges();
                _notyfService.Success("تغییرات با موفقیت ذخیره شد !");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyfService.Error("مشکلی در ارسال داده ها رخ داده است لطفا پارامتر های ورودی و نوع فایل تصویر ورودی را چک کنید !");
                return View();
            }
        }


        #endregion

        #region RemoveSlider

        // GET: SliderController/Delete/5
        public ActionResult Delete(int id)
        {
            var slideDetails = _context.Slider.Find(id);
            return View(slideDetails);
        }

        // POST: SliderController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                //Getting slide and removing it
                var slideDetails = _context.Slider.Find(id);
                _context.Slider.Remove(slideDetails);
                _context.SaveChanges();
                _notyfService.Success("اسلاید مورد نظر با موفقیت حذف گردید !");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

    }
}
