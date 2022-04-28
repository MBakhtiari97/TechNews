using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using TechNews.Core.DTOs;
using TechNews.Core.Services.Interfaces;

namespace TechNews.Web.Controllers
{
    public class Support : Controller
    {
        private ISupportRepository _supportRepository;
        public INotyfService _notyfService;

        public Support(ISupportRepository supportRepository,INotyfService notyfService)
        {
            _supportRepository = supportRepository;
            _notyfService = notyfService;
        }

        [Route("Ticket")]
        public IActionResult Ticketing()
        {
            return View();
        }
        [Route("Ticket")]
        [HttpPost]
        public IActionResult Ticketing(TicketViewModel ticket)
        {
            ticket.IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _supportRepository.SendTicket(ticket);
            _notyfService.Success("تیکت شما با موفقیت ارسال شد . با تشکر از تماس شما!");
            return View();
        }
    }
}
