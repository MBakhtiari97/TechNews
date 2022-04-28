using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TechNews.Core.DTOs;
using TechNews.Core.RequiredMethods;
using TechNews.Core.Services.Interfaces;
using TechNews.Web.Models;
using UAParser;

namespace TechNews.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICategoryRepository _categoryRepository;
        private IPostRepository _postRepository;
        private IReviewRepository _reviewRepository;
        private ISliderRepository _sliderRepository;
        private IBrowserRepository _browserRepository;

        public HomeController(ILogger<HomeController> logger,
            ICategoryRepository categoryRepository,
            IPostRepository postRepository,
            IReviewRepository reviewRepository,
            ISliderRepository sliderRepository,
            IBrowserRepository browserRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
            _reviewRepository = reviewRepository;
            _sliderRepository = sliderRepository;
            _browserRepository = browserRepository;
        }

        public IActionResult Index()
        {

            #region SideCategory

            GetSideCategories sideCategories = new GetSideCategories(_categoryRepository);
            ViewBag.AltCategories = sideCategories.GetSideCategory();

            #endregion

            #region RequiredViewBags

            ViewBag.LatestGameReviews = _postRepository.GetLatestGamesReviews();
            ViewBag.MostLikedReviews = _reviewRepository.GetMostLikedReviews();
            ViewBag.MostCommentedPosts = _postRepository.GetMostCommentedPosts();
            ViewBag.MostSeenPosts = _postRepository.GetMostSeenPostThisMonth();
            ViewBag.LatestJournals = _postRepository.GetLatestJournals();
            ViewBag.Recommended = _postRepository.GetLatestRecommended();
            ViewBag.LatestPosts = _postRepository.GetLatestPosts();
            ViewBag.LatestTechNews = _postRepository.GetLatestTechNews();
            ViewBag.Slider = _sliderRepository.GetSlides();

            #endregion

            #region ClientBrowserInfo

            //Getting client browser info for saving in database 
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo userBrowserName = uaParser.Parse(userAgent);
            //Saving Infos
            _browserRepository.InsertBrowserInfo(userBrowserName.ToString());

            #endregion

            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
