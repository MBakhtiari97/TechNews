using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechNews.Core.Services.Interfaces
{
    public interface IBrowserRepository
    {
        void InsertBrowserInfo(string browserName);
    }
}
