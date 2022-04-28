using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;

namespace TechNews.Core.RequiredMethods
{
    public class GetSideCategories
    {
        private ICategoryRepository _categoryRepository;

        public GetSideCategories(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        
        public List<ShowSideCategoryViewModel> GetSideCategory()
        {
            List<ShowSideCategoryViewModel> category = new List<ShowSideCategoryViewModel>();
            foreach (var categoryItem in _categoryRepository.GetAllCategories())
            {
                category.Add(new ShowSideCategoryViewModel()
                {
                    CategoryTitle = categoryItem.CategoryTitle,
                    CategoryCount = categoryItem.SelectedCategory.Count,
                    CategoryId = categoryItem.CategoryId
                });
            }

            return category;
        }
    }
}
