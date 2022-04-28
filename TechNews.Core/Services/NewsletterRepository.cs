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
    public class NewsletterRepository : INewsletterRepository
    {
        private TechNewsContext _context;
        public NewsletterRepository(TechNewsContext context)
        {
            _context = context;
        }
        public bool InsertToNewsLetter(string email)
        {

            _context.Newsletter.Add(new Newsletter()
            {
                EmailAddress = email
            });
            _context.SaveChanges();
            return true;

        }

        public bool IsAlreadyJoined(string email)
        {
            if (_context.Newsletter.Any(n => n.EmailAddress == email))
                return true;

            return false;

        }

        public bool RemoveFromNewsletter(string email)
        {
            var removeEmail = _context.Newsletter.Single(n => n.EmailAddress == email);

            _context.Newsletter.Remove(removeEmail);
            _context.SaveChanges();
            return true;


        }
    }
}
