using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Entities;

namespace TechNews.Web.Controllers
{
    public class NewsletterController : Controller
    {
        private INewsletterRepository _newsletterRepository;
        public INotyfService _notyfService;
        public NewsletterController(INewsletterRepository newsletterRepository,INotyfService notyfService)
        {
            _newsletterRepository = newsletterRepository;
            _notyfService = notyfService;
        }

        [Route("Newsletter")]
        public IActionResult JoinToNewsletter()
        {
            return View("_Newsletter");
        }
        [Route("Newsletter")]
        [HttpPost]
        public IActionResult JoinToNewsletter(string emailAddress)
        {
            if (_newsletterRepository.IsAlreadyJoined(emailAddress.ToLower().Trim()))
            {
                _newsletterRepository.RemoveFromNewsletter(emailAddress.ToLower().Trim());
                _notyfService.Success("سرویس خبرنامه برای شما غیرفعال شد !");
            }
            else
            {
                _newsletterRepository.InsertToNewsLetter(emailAddress.ToLower().Trim());
                _notyfService.Success("سرویس خبرنامه برای شما فعال شد !");
            }

            return View("_Newsletter");
        }
    }
}
