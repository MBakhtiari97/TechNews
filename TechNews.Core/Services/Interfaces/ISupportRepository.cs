using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.DTOs;

namespace TechNews.Core.Services.Interfaces
{
    public interface ISupportRepository
    {
        void SendTicket(TicketViewModel ticket);
    }
}
