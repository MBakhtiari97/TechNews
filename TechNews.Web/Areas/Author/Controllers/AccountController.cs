using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;

namespace TechNews.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class AccountController : Controller
    {
        private TechNewsContext _context;
        private INotyfService _notyfService;

        public AccountController(TechNewsContext context,INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

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
    }
}
