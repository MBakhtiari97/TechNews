using System.Linq;
using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private TechNewsContext _context;

        public HomeController(TechNewsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            #region RequiredViewBags

            ViewBag.UsersCount = _context.Users.Count();

            ViewBag.NewReviews = _context.Reviews.OrderByDescending(r => r.ReviewDate)
                .Count(r => !r.IsPublished);

            ViewBag.NewTicketsCount = _context.Tickets.OrderByDescending(r => r.AnswerSubmitDate)
                .Count(r => !r.IsAnswered);

            ViewBag.Browsers = _context.Browsers.ToList();

            ViewBag.NewTickets = _context.Tickets
                .OrderByDescending(t => t.TicketSubmitDate)
                .Where(t => !t.IsAnswered)
                .Take(6)
                .ToList();

            ViewBag.NewUsers = _context.Users.OrderByDescending(i => i.RegisterDate).Take(6).ToList();

            #endregion

            return View();
        }
    }
}
