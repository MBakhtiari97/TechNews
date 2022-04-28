using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;

namespace TechNews.Web.Areas.Author.Controllers
{
    [Area("Author")]
    public class HomeController : Controller
    {
        private TechNewsContext _context;

        public HomeController(TechNewsContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.NewReviews = _context.Reviews
                .OrderByDescending(r => r.ReviewDate)
                .Count(r => !r.IsPublished);

            ViewBag.PostAuthorCount = _context.Items
                .Count(i => i.AuthorId == int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
                    .ToString()));

            return View();
        }
    }
}
