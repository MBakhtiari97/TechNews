using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;
using TechNews.Utility.Generators;

namespace TechNews.Core.Services
{
    public class SupportRepository : ISupportRepository
    {
        private TechNewsContext _context;

        public SupportRepository(TechNewsContext context)
        {
            _context = context;
        }

        public void SendTicket(TicketViewModel ticket)
        {
            _context.Tickets.Add(new Ticket()
            {
                UserIpAddress = ticket.IpAddress,
                TicketDescription = ticket.TicketDescription,
                TicketSubmitDate = DateTime.Now,
                TicketTitle = ticket.TicketTitle,
                TicketUniqueCode = StringGenerator.GenerateUniqueString(),
                UserEmailAddress = ticket.UserEmailAddress,
                UserFullName = ticket.UserFullName,
                UserPhoneNumber = ticket.UserPhoneNumber
            });
            _context.SaveChanges();
        }
    }
}
