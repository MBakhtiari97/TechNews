using System;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;
using TechNews.Utility;
using TechNews.Web.Areas.Admin.Models.ViewModels;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        private IViewRenderService _viewRenderService;
        public INotyfService _notyfService { get; }

        public MailController(TechNewsContext context, IViewRenderService viewRenderService)
        {
            _context = context;
            _viewRenderService = viewRenderService;
        }

        #endregion

        #region Index

        public IActionResult Index(int pageId)
        {
            var emailHistory = _context.Emails.ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(emailHistory.Count() / (double)take);

            return View(emailHistory.Skip(skip).Take(take).ToList());
        }


        #endregion

        #region SendCustomEmail

        [Route("/Admin/SendEmail")]
        public IActionResult CustomEmailSend()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DefaultMailAsync(CustomEmailViewModel email)
        {
            //Specifying email body view and sending required model to it
            var body = await _viewRenderService.RenderToStringAsync("Email/_SendDefaultEmail", email);

            //Sending email with specified values
            SendEmail.Send(email.ReceiverEmailAddress.Trim().ToLower(), $"{email.EmailTitle}", body);

            //Creating a new email history and saving it on database
            var addToEmailHistory = new SentEmails()
            {
                Title = email.EmailTitle,
                Date = DateTime.Now,
                Receiver = email.ReceiverEmailAddress
            };
            _context.Emails.Add(addToEmailHistory);
            await _context.SaveChangesAsync();

            _notyfService.Success("ایمیل مورد نظر با موفقیت ارسال شد !");

            return RedirectToAction("Index");
        }


        #endregion

        #region SendNewsletterEmail

        [Route("/Admin/SendNewsletter")]
        public IActionResult NewsletterEmailSend()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsletterMailAsync(NewsletterEmailViewModel email)
        {
            //Specifying email body view and sending required model to it
            var body = await _viewRenderService.RenderToStringAsync("Email/_SendNewsletterEmail", email);

            //Sending email with specified values but this time for all subscribed newsletter users
            foreach (var emailAddress in _context.Newsletter.Select(e => e.EmailAddress))
            {
                SendEmail.Send(emailAddress, $"{email.EmailTitle}", body);
            }

            //Creating a new email history and saving it on database
            var addToEmailHistory = new SentEmails()
            {
                Title = email.EmailTitle,
                Date = DateTime.Now,
                Receiver = "سرویس خبرنامه"
            };

            _context.Emails.Add(addToEmailHistory);
            await _context.SaveChangesAsync();

            _notyfService.Success("ایمیل خبرنامه با موفقیت ارسال شد !");

            return RedirectToAction("Index");
        }

        #endregion

        #region RemoveEmailHistory

        public IActionResult Delete(int id)
        {
            var emailDetails = _context.Emails.Find(id);
            return View(emailDetails);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Finding email that want to delete and removing it
            var emailDetails = _context.Emails.Find(id);
            _context.Emails.Remove(emailDetails);
            _context.SaveChanges();
            _notyfService.Success("آیتم مورد نظر با موفقیت حذف شد !");

            return RedirectToAction("Index");
        }

        #endregion

    }
}
