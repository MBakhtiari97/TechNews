using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.RequiredMethods;
using TechNews.Core.Services.Interfaces;

namespace TechNews.Web.Controllers
{
    public class PostController : Controller
    {
        private IPostRepository _postRepository;
        private ICategoryRepository _categoryRepository;

        public PostController(IPostRepository postRepository,ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        [Route("Post/{itemId}")]
        public IActionResult ShowPost(int itemId)
        {
            return View("_SinglePost",_postRepository.GetItemByItemId(itemId));
        }
        [Route("Category/{categoryId}")]
        public IActionResult ShowPostByCategory(int categoryId,int pageId)
        {
            ViewBag.CategoryTitle = _categoryRepository.GetCategoryTitleByCategoryId(categoryId);

            GetSideCategories sideCategories = new GetSideCategories(_categoryRepository);
            ViewBag.AltCategories = sideCategories.GetSideCategory();

            var postsInCategory = _postRepository.GetPostByCategoryId(categoryId);
            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(postsInCategory.Count() / (double)take);

            return View("_ShowPostInCategories", postsInCategory.Skip(skip).Take(take).ToList());
        }

        [Route("LikePost/{itemId}")]
        public IActionResult LikePost(int itemId)
        {
            _postRepository.LikePost(itemId);
            return Redirect($"/Post/{itemId}");

        }
    }
}
