using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private TechNewsContext _context;

        public CategoryRepository(TechNewsContext context)
        {
            _context = context;
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories
                .Include(c => c.SelectedCategory)
                .ToList();
        }

        public string GetCategoryTitleByCategoryId(int categoryId)
        {
            return _context.Categories
                .Find(categoryId).CategoryTitle;
        }
    }
}
