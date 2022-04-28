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
    public class SliderRepository : ISliderRepository
    {
        private TechNewsContext _context;
        public SliderRepository(TechNewsContext context)
        {
            _context = context;
        }

        public IEnumerable<Slider> GetSlides()
        {
            return _context.Slider
                .Where(s => s.IsActive)
                .ToList();
        }
    }
}
