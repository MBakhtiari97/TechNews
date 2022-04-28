using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Entities;
using TechNews.Utility;
using TechNews.Utility.Generators;
using TechNews.Utility.Security;

namespace TechNews.Web.Controllers
{
    public class Account : Controller
    {

        #region RepositoryInjection

        private IUserRepository _userRepository;
        private IViewRenderService _viewRenderService;
        public INotyfService _notyfService;

        public Account(IUserRepository userRepository, IViewRenderService viewRenderService, INotyfService notyfService)
        {
            _userRepository = userRepository;
            _viewRenderService = viewRenderService;
            _notyfService = notyfService;
        }

        #endregion

        [BindProperty]
        public ChangeUserDetailsViewModel Details { get; set; }

        #region SignUp

        [Route("SignUp")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
                return View(register);

            if (!_userRepository.IsEmailExisted(register.EmailAddress))
            {
                ModelState.AddModelError("EmailAddress", "آدرس ایمیل تکراری است ، لطفا از آدرس ایمیل دیگری برای ثبت نام استفاده نمایید !");
                return View(register);
            }

            //Creating an instance of user and filling it
            User User = new User()
            {
                EmailAddress = register.EmailAddress.Trim().ToLower(),
                RoleId = 1,
                IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                IdentifierCode = StringGenerator.GenerateUniqueString(),
                IsActive = false,
                PhoneNumber = register.PhoneNumber,
                ProfilePhoto = "Default.png",
                RegisterDate = DateTime.Now,
                UserName = register.Username,
                Password = PasswordHelper.EncodePasswordMd5(register.Password)
            };

            _userRepository.RegisterUser(User);
            _notyfService.Success("ثبت نام شما با موفقیت انجام شد ! لطفا به آدرس ایمیل خود مراجعه کرده و حساب کاربری خود را فعال نمایید !");

            var body = await _viewRenderService.RenderToStringAsync("Account/Email/_ActivationEmail", User);
            SendEmail.Send(register.EmailAddress, "ایمیل فعالسازی", body);

            return RedirectToAction("SignIn");
        }

        #endregion

        #region ActiveUser
        [Route("Active/{email}/{identifier}")]
        public IActionResult ActivateUser(string email, string identifier)
        {
            if (_userRepository.ActivateUser(email, identifier))
            {
                _notyfService.Success("حساب کاربری شما با موفقیت فعال شد ! هم اکنون میتوانید از تمامی بخش های سایت استفاده کنید !");
            }
            else
            {
                _notyfService.Error("مشکلی در فعالسازی حساب کاربری شما به وجود آمده است و یا شما قبلا حساب کاربری خود را فعال کرده اید !");
            }

            return Redirect("/");
        }

        #endregion

        #region SignIn
        [Route("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("SignIn")]
        [HttpPost]
        public IActionResult SignIn(LoginViewModel login)
        {
            if (!ModelState.IsValid)
                return View(login);

            var user = _userRepository.GetUserForLogin(login.EmailAddress.Trim().ToLower(),
                PasswordHelper.EncodePasswordMd5(login.Password));

            if (user == null)
            {
                ModelState.AddModelError("EmailAddress", "کاربری با مشخصات وارد شده یافت نشد !");
                return View();
            }
            else
            {
                if (user.IsActive == true)
                {
                    //Logging user codes
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                        new Claim(ClaimTypes.Email, user.EmailAddress),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("IsAdmin", user.RoleId.ToString()),
                        new Claim("ProfilePhoto", user.ProfilePhoto)
                    };

                    var identifier = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identifier);
                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);
                    _notyfService.Success("ورود موفقیت آمیز بود !");
                }
                else
                {
                    ModelState.AddModelError("EmailAddress","حساب کاربری شما فعال نیست ! لطفا جهت دسترسی به حساب کاربری خود به آدرس ایمیل وارد شده هنگام ثبت نام مراجعه کرده و برروی لینک فعالسازی کلیک نمایید !");
                    return View(login);
                }
            }
            return Redirect("/");
        }

        #endregion

        #region SignOut

        [Route("SignOut")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion

        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordViewModel forgot)
        {
            var user = _userRepository.GetUserByEmail(forgot.EmailAddress.Trim().ToLower());
            if (user == null)
            {
                _notyfService.Error("کاربری با آدرس ایمیل وارد شده یافت نشد !");
                return View(forgot);
            }

            var body = await _viewRenderService.RenderToStringAsync("Account/Email/_ForgotPasswordEmail", user);
            SendEmail.Send(forgot.EmailAddress.Trim().ToLower(),"بازیابی کلمه عبور",body);
            _notyfService.Success("ایمیل بازیابی کلمه عبور به آدرس ایمیل شما ارسال شد !");
            return View();
        }
        [Route("RecoverPassword/{emailAddress}/{identifierCode}")]
        public IActionResult RecoverPassword(string emailAddress,string identifierCode)
        {
            var user = _userRepository.GetUserByIdentifierCode(identifierCode, emailAddress);
            if (user == null)
            {
                _notyfService.Error("درخواست نامعتبر !");
                return RedirectToAction("SignIn");
            }

            user.IdentifierCode = StringGenerator.GenerateUniqueString();
            user.IsActive = true;
            var newPassword = StringGenerator.GenerateRandomPassword();
            user.Password = PasswordHelper.EncodePasswordMd5(newPassword);
            _userRepository.SaveChanges();
            ViewBag.Username = user.UserName;
            ViewBag.Password = newPassword;

            return View();
        }

        #endregion

        #region UserPanel

        [Route("/UserPanel")]
        [Authorize]
        public IActionResult UserPanel(int userId)
        {
            var userDetails = _userRepository.GetUserByUserId(userId);
            return View(userDetails);
        }
        [Authorize]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (_userRepository.ChangeUserPassword(changePassword))
            {
                _notyfService.Success("کلمه عبور شما با موفقیت تغییر کرد !");
            }
            else
            {
                _notyfService.Error("کاربری با مشخصات وارد شده یافت نشد ! لطفا فیلد کلمه عبور فعلی خود را بررسی کنید !");
            }
            return View();
        }

        [Authorize]
        [Route("UserPanel/ChangeDetails")]
        public IActionResult ChangeUserDetails(int userId)
        {
            var userDetails = _userRepository.GetUserByUserId(userId);
            return View(userDetails);
        }
        [HttpPost]
        [Authorize]
        [Route("UserPanel/ChangeDetails")]
        public IActionResult ChangeUserDetails(int userId, [Bind("UserId", "UserName", "EmailAddress", "PhoneNumber", "ProfilePhoto", "IpAddress", "RegisterDate", "IsActive", "IdentifierCode", "RoleId", "Password")] User users)
        {
            //Checking for model validation
            if (!ModelState.IsValid)
                return View();

            //Getting current user details
            var userCurrentDetails = _userRepository.GetUserByUserId(userId);
            if (userCurrentDetails == null)
                return NotFound();

            //Checking if user wants to change the profile photo
            if (Details.ProfilePhoto?.Length > 0 && Details.ProfilePhoto.IsImage())
            {
                //Checking if the name wasnt default then deleting it
                if (userCurrentDetails.ProfilePhoto != "Default.png")
                {
                    var oldPath = Path.Combine(Directory.GetCurrentDirectory() ,
                                               "wwwroot" ,
                                               "img" ,
                                               "users" ,
                                               userCurrentDetails.ProfilePhoto);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                //Creating new path and saving image on this path on server
                var newImageName = StringGenerator.GenerateUniqueString();
                var newPath = Path.Combine(Directory.GetCurrentDirectory() ,
                                           "wwwroot" ,
                                           "img" ,
                                           "users" ,
                                           newImageName + 
                                           Path.GetExtension(Details.ProfilePhoto.FileName));

                users.ProfilePhoto = newImageName +
                                     Path.GetExtension(Details.ProfilePhoto.FileName);

                using var stream = new FileStream(newPath, FileMode.Create);
                Details.ProfilePhoto.CopyTo(stream);

            }
            else
            {
                users.ProfilePhoto = userCurrentDetails.ProfilePhoto;
            }

            //Filling other required details
            users.IdentifierCode = userCurrentDetails.IdentifierCode;
            users.IsActive = userCurrentDetails.IsActive;
            users.RegisterDate = userCurrentDetails.RegisterDate;
            users.RoleId = userCurrentDetails.RoleId;
            users.IpAddress = userCurrentDetails.IpAddress;

            //Trying to update details
            if (_userRepository.UpdateUser(users))
            {
                _notyfService.Success("اطلاعات کاربری با موفقیت بروز شد !");
            }
            else
            {
                _notyfService.Error("مشکلی در ذخیره اطلاعات پیش آمده است !");
            }
            return Redirect($"/UserPanel?userId={users.UserId}");
        }
        #endregion
    }
}
