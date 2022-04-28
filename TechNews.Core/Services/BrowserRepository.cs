using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services
{
    public class BrowserRepository : IBrowserRepository
    {
        #region Injection

        private TechNewsContext _context;

        public BrowserRepository(TechNewsContext context)
        {
            _context = context;
        }

        #endregion

        #region InsertInfo

        public void InsertBrowserInfo(string browserName)
        {
            //Checking if the browser info doesn't existed on database then creating it
            if (!_context.Browsers.Any(b => b.BrowserName == browserName))
            {
                _context.Browsers.Add(new Browser()
                {
                    BrowserName = browserName,
                    UsedCount = 0
                });
                _context.SaveChanges();
            }

            //Getting Browser Info
            var browserInfo = _context.Browsers.SingleOrDefault(b => b.BrowserName == browserName);

            //Adding to used count of it
            if (browserInfo != null)
            {
                browserInfo.UsedCount += 1;
                _context.SaveChanges();
            }
            else
            {

            }
        }

        #endregion

    }
}
