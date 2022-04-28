using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using TechNews.Core.DTOs;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;
using TechNews.Utility.Generators;
using TechNews.Web.Areas.Admin.Models.ViewModels;

namespace TechNews.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class PostController : Controller
    {
        #region Injection

        private TechNewsContext _context;

        public PostController(TechNewsContext context)
        {
            _context = context;
        }


        #endregion

        #region BindingProperties

        //Binding properties in order to accessing selected photo by user 
        [BindProperty]
        public List<int> SelectedGroups { get; set; }

        [BindProperty]
        public PostViewModel Post { get; set; }


        #endregion

        #region Index

        // GET: PostController
        public ActionResult Index(int pageId)
        {
            var posts = _context.Items
                .Where(i => i.AuthorId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()))
                .ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(posts.Count() / (double)take);

            return View(posts.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region Details

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            var posts = _context.Items.SingleOrDefault(i =>
                i.AuthorId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString()) && i.ItemId == id);

            return View(posts);
        }


        #endregion

        #region CreeateNewPost

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("ItemId,ItemTitle,ItemDescription,ShortDescription,ItemImage,LikeCount,ItemAuthor,ItemSubmitDate,TagId,ReviewId")] Item items)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(items);
                }

                //Generating a new name for image 
                var postImageName = StringGenerator.GenerateUniqueString();

                //Checking if the user was selected an image then name it by the name that generated currently
                if (Post.ItemImage?.Length > 0)
                    items.ItemImage = postImageName
                                      + Path.GetExtension(Post.ItemImage.FileName);
                //If user wasn't choose an image then name it to default.png
                else
                {
                    items.ItemImage = "Default.png";
                }

                //Specifying required values and adding post to database
                items.LikeCount = 0;
                items.ItemSubmitDate = DateTime.Now;
                items.AuthorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                _context.Add(items);
                _context.SaveChanges();

                //Checking if image was selected then we should copy that image on server
                if (Post.ItemImage?.Length > 0)
                {
                    var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "posts",
                        postImageName
                        + Path.GetExtension(Post.ItemImage.FileName)
                    );
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Post.ItemImage.CopyTo(stream);
                    }
                }

                //adding selected categories for this post on database
                if (SelectedGroups.Any() && SelectedGroups.Count > 0)
                {
                    foreach (var CategoryId in SelectedGroups)
                    {
                        _context.SelectedCategories.Add(new SelectedCategory()
                        {
                            ItemId = items.ItemId,
                            CategoryId = CategoryId
                        });
                    }
                }
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region UpdatePost

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            //Getting post and selected categories for that in order to show it on update page
            var post = _context.Items.Find(id);
            if (post == null)
                return NotFound();

            ViewBag.SelectedCategories = _context.SelectedCategories
                .Where(c => c.ItemId == id)
                .Select(c => c.CategoryId)
                .ToList();

            return View(post);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("ItemId,ItemTitle,ItemDescription,ShortDescription,ItemImage,LikeCount,ItemAuthor,ItemSubmitDate,TagId,ReviewId")] Item items)
        {
            if (id != items.ItemId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(items);
            }

            try
            {
                //Deleting current selected categories
                if (SelectedGroups.Any() && SelectedGroups.Count > 0)
                {
                    foreach (var currentCategory in _context.SelectedCategories.Where(c => c.ItemId == id))
                    {
                        _context.Remove(currentCategory);
                    }
                }
                //Creating new selected categories
                if (SelectedGroups.Any() && SelectedGroups.Count > 0)
                {
                    foreach (var CategoryId in SelectedGroups)
                    {
                        _context.SelectedCategories.Add(new SelectedCategory()
                        {
                            ItemId = items.ItemId,
                            CategoryId = CategoryId
                        });
                    }
                }
                _context.SaveChanges();

                //Gathering current item for some details like image name
                var currentItemDetails = _context.Items.AsNoTracking()
                    .FirstOrDefault(pg => pg.ItemId == id);

                //Checking if user want to change the image
                if (Post.ItemImage != null)
                {
                    string oldFilePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "posts",
                        currentItemDetails.ItemImage
                    );
                    //Deleting existed image
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }

                    //Giving new name to image and saving it on server
                    var newImgName = StringGenerator.GenerateUniqueString();
                    var newFilePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "posts",
                        newImgName + Path.GetExtension(Post.ItemImage.FileName)
                    );


                    items.ItemImage = newImgName
                                                 + Path.GetExtension(Post.ItemImage.FileName);
                    using var stream = new FileStream(newFilePath, FileMode.Create);
                    Post.ItemImage.CopyTo(stream);
                }
                else
                {
                    items.ItemImage = currentItemDetails.ItemImage;
                }

                if (items.AuthorId == null)
                {
                    items.AuthorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                }

                _context.Update(items);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(items);
            }
        }


        #endregion

        #region RemovePost

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            var post = _context.Items.Find(id);
            if (post == null)
                return NotFound();

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var post = _context.Items.Find(id);

                //Getting current post image directory
                string currentPostImage = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "img",
                    "posts",
                    post.ItemImage
                );

                //Deleting existed image
                if (System.IO.File.Exists(currentPostImage))
                {
                    System.IO.File.Delete(currentPostImage);
                }

                //Deleting selected categories
                if (_context.SelectedCategories.Any(c => c.ItemId == id))
                {
                    foreach (var currentCategory in _context.SelectedCategories.Where(c => c.ItemId == id))
                    {
                        _context.SelectedCategories.Remove(currentCategory);
                    }
                }
                _context.SaveChanges();

                //Removing post(item)
                _context.Items.Remove(post);
                _context.SaveChanges();

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
