using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechNews.Core.DTOs;
using TechNews.DataLayer.Entities;

namespace TechNews.Core.Services.Interfaces
{
    public interface IUserRepository
    {
        void RegisterUser(User newUser);
        bool IsEmailExisted(string email);
        bool ActivateUser(string email, string identifier);
        User GetUserForLogin(string email, string password);
        User GetUserByEmail(string email);
        User GetUserByIdentifierCode(string identifierCode,string emailAddress);
        bool ChangeUserPassword(ChangePasswordViewModel newPassword);
        User GetUserByUserId(int userId);
        bool UpdateUser(User user);
        void SaveChanges();
    }
}
