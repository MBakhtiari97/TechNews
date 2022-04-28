using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services.Interfaces
{
    public interface INewsletterRepository
    {
        bool InsertToNewsLetter(string email);
        bool IsAlreadyJoined(string email);
        bool RemoveFromNewsletter(string email);
    }
}
