using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Entities;

namespace TechNews.Web.Controllers
{
    public class SearchController : Controller
    {
        private IPostRepository _postRepository;

        public SearchController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        [Route("Search")]
        public IActionResult SearchPage()
        {
            return View("_SearchPage");
        }
        [Route("Search")]
        [HttpPost]
        public IActionResult SearchPage(string searchPhrase , int pageId)
        {
            ViewBag.PhrasedSearched = searchPhrase;
            var result = _postRepository.GetPostForSearch(searchPhrase);

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(result.Count() / (double)take);

            return View("_SearchResult", result.Skip(skip).Take(take).ToList());
        }
        public IActionResult TagPage(string tagPhrase)
        {
            ViewBag.PhrasedSearched = tagPhrase;
            return View("_SearchResult", _postRepository.GetPostForSearch(tagPhrase));
        }
    }
}
