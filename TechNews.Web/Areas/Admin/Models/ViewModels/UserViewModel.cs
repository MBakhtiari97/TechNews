using System;
using Microsoft.AspNetCore.Http;

namespace TechNews.Web.Areas.Admin.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string IpAddress { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsActive { get; set; }
        public string IdentifierCode { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public IFormFile ProfilePhoto { get; set; }

    }
}
