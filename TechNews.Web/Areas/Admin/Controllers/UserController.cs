using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;
using TechNews.Utility;
using TechNews.Utility.Generators;
using TechNews.Utility.Security;
using TechNews.Web.Areas.Admin.Models.ViewModels;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        private IViewRenderService _viewRenderService;
        public INotyfService _notyfService;

        public UserController(TechNewsContext context, IViewRenderService viewRenderService, INotyfService notyfService)
        {
            _context = context;
            _viewRenderService = viewRenderService;
            _notyfService = notyfService;
        }

        #endregion

        #region BindingProperties

        [BindProperty]
        public UserViewModel NewUser { get; set; }


        #endregion

        #region Index

        // GET: User
        public ActionResult Index(int pageId)
        {
            var users = _context.Users.ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(users.Count() / (double)take);


            return View(users.Skip(skip).Take(take).ToList());
        }

        #endregion

        #region Details

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        #endregion

        #region CreateNewUser

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("UserId", "UserName", "EmailAddress", "PhoneNumber", "ProfilePhoto", "IpAddress", "RegisterDate", "IsActive", "IdentifierCode", "RoleId", "Password")] User users)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(users);

                //Checking if the email was currently existed in the database then throw a massage and do not let registering user
                if (_context.Users.Any(u => u.EmailAddress == users.EmailAddress.ToLower()))
                {
                    _notyfService.Error("آدرس ایمیل تکراری است !");
                    return View(users);
                }

                //Generating new name for profile photo
                var userProfilePhotoName = StringGenerator.GenerateUniqueString();

                //If photo was selected then name it by currently generated name otherwise name it to default.png
                if (NewUser.ProfilePhoto?.Length > 0 && NewUser.ProfilePhoto.IsImage())
                {
                    users.ProfilePhoto = userProfilePhotoName
                                         + Path.GetExtension(NewUser.ProfilePhoto.FileName);
                }
                else
                {
                    users.ProfilePhoto = "Default.png";
                }

                //Filling required values and inserting user
                users.EmailAddress = users.EmailAddress.ToLower();
                users.RegisterDate = DateTime.Now;

                //Hashing password in order to storing in config file
                users.Password = PasswordHelper.EncodePasswordMd5(users.Password);
                users.IdentifierCode = StringGenerator.GenerateUniqueString();

                _context.Users.Add(users);
                await _context.SaveChangesAsync();

                //If the photo was selected then we copying it to server
                if (NewUser.ProfilePhoto?.Length > 0 && NewUser.ProfilePhoto.IsImage())
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "users",
                        userProfilePhotoName + Path.GetExtension(NewUser.ProfilePhoto.FileName)
                        );
                    await using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await NewUser.ProfilePhoto.CopyToAsync(stream);
                    }
                }

                //Checking if the is active wasn't checked then sending an activation email to user email address
                if (!users.IsActive)
                {
                    var body = await _viewRenderService.RenderToStringAsync("Account/Email/_ActivationEmail", users);
                    SendEmail.Send(users.EmailAddress, "ایمیل فعالسازی", body);
                    _notyfService.Success("ساخت حساب کاربری مورد با موفقیت انجام شد و ایمیل فعالسازی برای کاربر ارسال شد !");
                }


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyfService.Error("مشکلی در ارسال داده ها رخ داده است لطفا پارامتر های ورودی و نوع فایل تصویر ورودی را چک کنید !");
                return View();
            }
        }


        #endregion

        #region UpdateUserDetails

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("UserId", "UserName", "EmailAddress", "PhoneNumber", "ProfilePhoto", "IpAddress", "RegisterDate", "IsActive", "IdentifierCode", "RoleId", "Password")] User users)
        {
            try
            {
                if (id != users.UserId)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return View(users);
                }


                //Getting current old user details
                var currentUserDetails = _context.Users.AsNoTracking()
                    .FirstOrDefault(pg => pg.UserId == id);

                //Checking if user changed the email address then program doesn't let submit a duplicate email address
                if (currentUserDetails.EmailAddress != users.EmailAddress.ToLower())
                {
                    if (_context.Users.Any(u => u.EmailAddress == users.EmailAddress.ToLower()))
                    {
                        _notyfService.Error("آدرس ایمیل تکراری است !");
                        return View(users);
                    }
                    users.EmailAddress = users.EmailAddress.ToLower();
                }

                //Checking if user currently has any photo that is not default then go for delete it
                if (currentUserDetails.ProfilePhoto != "Default.png")
                {
                    //Checking if user want to change the image
                    if (NewUser.ProfilePhoto != null && NewUser.ProfilePhoto.IsImage())
                    {
                        string oldFilePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "img",
                            "users",
                            currentUserDetails.ProfilePhoto
                        );
                        //Deleting existed image
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }

                    }


                    //Giving new name to image and saving it on server
                    var newImgName = StringGenerator.GenerateUniqueString();
                    var newFilePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "img",
                        "users",
                        newImgName + Path.GetExtension(NewUser.ProfilePhoto.FileName)
                    );


                    users.ProfilePhoto = newImgName
                                      + Path.GetExtension(NewUser.ProfilePhoto.FileName);

                    using var stream = new FileStream(newFilePath, FileMode.Create);
                    NewUser.ProfilePhoto.CopyTo(stream);
                }
                else
                {
                    users.ProfilePhoto = currentUserDetails.ProfilePhoto;
                }

                //Generating new unique identifier code and updating user
                users.IdentifierCode = StringGenerator.GenerateUniqueString();

                _context.Update(users);
                _context.SaveChanges();
                _notyfService.Success("اطلاعات کاربر با موفقیت تغییر یافت");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _notyfService.Error("مشکلی در ارسال داده ها رخ داده است لطفا پارامتر های ورودی و نوع فایل تصویر ورودی را چک کنید !");
                return View();
            }
        }


        #endregion

        #region RemovingUser

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var user = _context.Users.Find(id);

                //Getting current post image directory
                string currentPostImage = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "img",
                    "posts",
                    user.ProfilePhoto
                );

                //Deleting existed image
                if (System.IO.File.Exists(currentPostImage))
                {
                    System.IO.File.Delete(currentPostImage);
                }

                //Removing post(item)
                _context.Users.Remove(user);
                _context.SaveChanges();

                _notyfService.Success("کاربر مورد نظر با موفقیت حذف شد !");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region AccountDetails

        public IActionResult GetAccountDetails(int userId)
        {
            var userDetails = _context.Users.Find(userId);
            if (userDetails == null)
            {
                _notyfService.Error("متاسفانه اطلاعات حساب کاربری شما دریافت نشد !");
                return RedirectToAction("Index", "Home");
            }

            return View(userDetails);
        }

        #endregion

        #region BlackList

        public IActionResult AddToBlackListUsers(string ipAddress)
        {
            if (!_context.BlackList.Any(u => u.BlackListIpAddress == ipAddress))
            {
                _context.BlackList.Add(new BlackList()
                {
                    BlackListIpAddress = ipAddress
                });
                _context.SaveChanges();
                _notyfService.Success("آیپی کاربر مورد نظر به لیست سیاه کاربران اضافه شد !");

            }
            else
            {
                _notyfService.Warning("آیپی آدرس کاربر مورد نظر در حال حاضر در لیست سیاه قرار گرفته !");
            }

            return RedirectToAction("Index");
        }
        [Route("/Admin/BlackList")]
        public IActionResult BlackListIndex()
        {
            var blackListUsers = _context.BlackList.ToList();
            return View(blackListUsers);
        }
        public IActionResult RemoveFromBlackList(int id)
        {
            var blackListUser = _context.BlackList.Find(id);

            _context.BlackList.Remove(blackListUser);
            _context.SaveChanges();
            _notyfService.Success("کاربر با موفقیت از لیست سیاه حذف شد !");

            return RedirectToAction("BlackListIndex");
        }

        #endregion
    }
}
