using System;
using System.Linq;
using AspNetCore;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechNews.DataLayer.Context;
using TechNews.DataLayer.Entities;

namespace TechNews.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        #region Injection

        private TechNewsContext _context;
        public INotyfService _notyfService { get; set; }


        public TicketController(TechNewsContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }

        #endregion

        #region Index

        // GET: TicketController
        public ActionResult Index(int pageId)
        {
            var tickets = _context.Tickets.ToList();

            //For Pagination
            int take = 9;
            int skip = (pageId - 1) * take;
            ViewBag.PageCount = (int)Math.Ceiling(tickets.Count() / (double)take);

            return View(tickets.Skip(skip).Take(take).ToList());
        }

        // GET: TicketController
        public ActionResult UnansweredTickets()
        {
            var tickets = _context.Tickets.Where(t=>!t.IsAnswered).ToList();
            return View("Index",tickets);
        }
        #endregion

        #region Details

        // GET: TicketController/Details/5
        public ActionResult Details(int id)
        {
            var ticketDetails = _context.Tickets.Find(id);
            if (ticketDetails == null)
                return NotFound();

            return View(ticketDetails);
        }


        #endregion

        #region UpdateTicketStatus

        // GET: TicketController/Edit/5
        public ActionResult Edit(int id)
        {
            var ticket = _context.Tickets.Find(id);
            return View(ticket);
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("TicketId", "UserFullName", "UserIpAddress", "UserEmailAddress", "UserPhoneNumber", "TicketTitle", "TicketDescription", "TicketSubmitDate", "TicketUniqueCode", "IsAnswered", "AnswerDescription", "AnswerSubmitDate")] Ticket ticket)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(ticket);
                }

                if (ticket.AnswerSubmitDate == null)
                {
                    ticket.AnswerSubmitDate = DateTime.Now;
                }
                _context.Tickets.Update(ticket);
                _context.SaveChanges();

                _notyfService.Success("وضعیت تیکت با موفقیت به روزرسانی شد !");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        #endregion

        #region RemoveTicket

        // GET: TicketController/Delete/5
        public ActionResult Delete(int id)
        {
            var ticketDetails = _context.Tickets.Find(id);

            return View(ticketDetails);
        }

        // POST: TicketController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var ticketDetails = _context.Tickets.Find(id);

                _context.Tickets.Remove(ticketDetails);
                _context.SaveChanges();
                _notyfService.Success("تیکت مورد نظر با موفقیت حذف شد !");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        #endregion

    }
}
