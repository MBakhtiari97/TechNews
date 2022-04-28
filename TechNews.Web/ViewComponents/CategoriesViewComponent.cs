using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.Services.Interfaces;

namespace TechNews.Web.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoriesViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;   
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/HeaderViewComponent.cshtml", _categoryRepository.GetAllCategories());

        }
    }
}
